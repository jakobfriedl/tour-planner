using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Windows;
using TourPlanner.DataAccessLayer.SQL;
using System.IO;

namespace TourPlanner.BusinessLayer
{
    public class SummarizeReportGeneration
    {
        private ObservableCollection<Log>? Logs;
        private ObservableCollection<Tour>? Tours;
        public void SummarizeReportGenerator(ObservableCollection<Tour> tours, ObservableCollection<Log> logs)
        {
            StatDAO stats = new StatDAO(new Database());
            Logs = logs;
            Tours = tours;
            var ReportPath = "Resources/reports/";

            if (!Directory.Exists(ReportPath))
            {
                Directory.CreateDirectory(ReportPath);
            }

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Header().AlignMiddle().AlignCenter()
                        .Text($"Summarized Reports")
                        .SemiBold().FontSize(30).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Column(column =>
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(15);
                                    columns.RelativeColumn(16);
                                    columns.RelativeColumn(12);
                                    columns.RelativeColumn(12);
                                    columns.RelativeColumn(12);
                                });
                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Tour");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Route");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight().Text("avg. Rating");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight().Text("avg. Difficulty");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight().Text("avg. Duration");
                                });
                                foreach (Tour t in Tours)
                                {
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text(t.Name).FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text($"{t.Start} - {t.Destination}").FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text(stats.GetAvgRating(t.Id)).FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text(stats.GetAvgDifficulty(t.Id)).FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text(TimeSpan.FromSeconds(stats.GetAvgDuration(t.Id))).FontSize(12);
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
            document.GeneratePdf($"{ReportPath}summarized.pdf");
        }
    }
}