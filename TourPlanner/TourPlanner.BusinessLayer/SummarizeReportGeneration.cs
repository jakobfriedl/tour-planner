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

namespace TourPlanner.BusinessLayer
{
    public class SummarizeReportGeneration
    {
        private ObservableCollection<Log>? Logs;
        private ObservableCollection<Tour>? Tours;
        public void SummarizeReportGenerator(ObservableCollection<Tour> tours, ObservableCollection<Log> logs)
        {
            Logs = logs;
            Tours = tours;
            var ReportPath = "Resources/reports/";

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
                                    columns.RelativeColumn(18);
                                    columns.RelativeColumn(13);
                                    columns.RelativeColumn(13);
                                    columns.RelativeColumn(8);
                                });
                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Route");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Destination");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight().Text("avg. Duration");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight().Text("avg. Distance");
                                    header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight().Text("Rating");
                                });
                                foreach (Tour t in Tours)
                                {
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text(t.Name).FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text($"{t.Start} - {t.Destination}").FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text(calculateAverageDuration(t.Id)).FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text(Math.Round(t.Distance, 2) + " km").FontSize(12);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight().Text(calculateAverageRating(t.Id)).FontSize(12);
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

        public string calculateAverageDuration(int id)
        {
            int avgDuration = 0;
            var logs = 0;
            foreach (var log in Logs)
            {
                if (log.TourId == id)
                {
                    avgDuration += log.TotalTime;
                    logs++;
                }
            }
            if (logs > 0)
            {
                avgDuration = avgDuration / logs;
                return FormatTime(avgDuration).ToString("G").Split(',')[0];
            }
            Tour? tour = Tours.Single(i => i.Id == id);
            return tour.DisplayTime.ToString();
        }
        public int calculateAverageRating(int id)
        {
            int avgRating = 0;
            var logs = 0;
            foreach (var log in Logs)
            {
                if (log.TourId == id)
                {
                    avgRating += log.Rating;
                    logs++;
                }
            }
            if (logs > 0)
            {
                avgRating = avgRating / logs;
                return avgRating;
            }
            Tour tour = Tours.Single(i => i.Id == id);
            return (int)tour.ChildFriendliness;
        }

        private TimeSpan FormatTime(int time)
        {
            var s = time % 60;
            var m = (time / 60) % 60;
            var h = (time / 3600);
            return new TimeSpan(h, m, s);
        }
    }
}