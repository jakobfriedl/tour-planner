using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer.ReportGeneration
{
    public class TourReportGenerator
    {
        /// <summary>
        /// creates the PDF Report for a specific Tour and saves it in "bin/Debug/Resources/reports/"
        /// </summary>
        /// <param name="selectedTour"></param>
        /// <param name="logs"></param>
        public void CreateTourReport(ILogger logger, Tour selectedTour, ObservableCollection<Log> logs)
        {
            var tourImg = File.ReadAllBytes(selectedTour.ImagePath);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Header().AlignMiddle().AlignCenter()
                        .Text($"{selectedTour.Name}")
                        .SemiBold().FontSize(30).FontColor(Colors.Blue.Medium);
                    page.Content()
                        .Column(column =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(x =>
                                {
                                    x.Item().Text($"Description: {selectedTour.Description}");
                                    x.Item().Text($"Start: {selectedTour.Start}");
                                    x.Item().Text($"Destination: {selectedTour.Destination}");
                                    x.Item().Text($"Distance: {selectedTour.DisplayDistance}");
                                    x.Item().Text($"Estimated Time: {selectedTour.DisplayTime}");
                                    x.Item().Text($"Transport Type: {selectedTour.TransportType}");
                                    x.Item().Text($"Popularity : {selectedTour.Popularity}");
                                    x.Item().Text($"Child Friendliness: {selectedTour.ChildFriendliness}");
                                });
                                row.ConstantItem(200).Height(200).Image(tourImg, ImageScaling.FitArea);
                            });
                            column.Item().AlignLeft().Text("Logs:").SemiBold().FontSize(20)
                                .FontColor(Colors.Blue.Medium);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(10);
                                    columns.RelativeColumn(8);
                                    columns.RelativeColumn(20);
                                    columns.RelativeColumn(5);
                                    columns.RelativeColumn(4);
                                });
                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Start Date/Time");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Tour Duration");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).PaddingHorizontal(8)
                                        .AlignLeft().Text("Comment");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight()
                                        .Text("Difficulty");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight().Text("Rating");
                                });
                                foreach (var log in logs)
                                {
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text(log.StartTime)
                                        .FontSize(10);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text(log.DisplayTime)
                                        .FontSize(10);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingHorizontal(8)
                                        .AlignLeft().Text(log.Comment).FontSize(10);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight()
                                        .Text(log.Difficulty).FontSize(10);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight()
                                        .Text(log.Rating).FontSize(10);
                                }
                            });
                        });
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });
            document.GeneratePdf(Path.Combine(ConfigManager.GetConfig().ReportLocation, $"{selectedTour.Id}.pdf"));
            logger.LogInformation($"Created tour-report for tour [id: {selectedTour.Id}]. {DateTime.UtcNow}");
        }
    }
}