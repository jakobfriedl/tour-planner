using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer {
	public class SummarizeReportGenerator {
		private ObservableCollection<Log>? _logs;
		private ObservableCollection<Tour>? _tours;

		public void CreateSummarizeReport(ILogger logger, ObservableCollection<Tour> tours, ObservableCollection<Log> logs) {
			var stats = new StatDAO(new Database(), logger);
			_logs = logs;
			_tours = tours;

			var document = Document.Create(container => {
				container.Page(page => {
					page.Size(PageSizes.A4);
					page.Margin(2, Unit.Centimetre);
					page.PageColor(Colors.White);
					page.DefaultTextStyle(x => x.FontSize(14));

					page.Header().AlignMiddle().AlignCenter()
						.Text($"Summarized Reports")
						.SemiBold().FontSize(30).FontColor(Colors.Blue.Medium);

					page.Content()
						.Column(column => {
							column.Item().Table(table => {
								table.ColumnsDefinition(columns => {
									columns.RelativeColumn(15);
									columns.RelativeColumn(16);
									columns.RelativeColumn(12);
									columns.RelativeColumn(12);
									columns.RelativeColumn(12);
								});
								table.Header(header => {
									header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Tour");
									header.Cell().BorderBottom(2).BorderColor(Colors.Black).Text("Route");
									header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight()
										.Text("avg. Rating");
									header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight()
										.Text("avg. Difficulty");
									header.Cell().BorderBottom(2).BorderColor(Colors.Black).AlignRight()
										.Text("avg. Duration");
								});
								foreach (var t in _tours) {
									table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Text(t.Name)
										.FontSize(12);
									table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
										.Text($"{t.Start} - {t.Destination}").FontSize(12);
									table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight()
										.Text(stats.GetAvgRating(t.Id)).FontSize(12);
									table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight()
										.Text(stats.GetAvgDifficulty(t.Id)).FontSize(12);
									table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignRight()
										.Text(TimeSpan.FromSeconds(stats.GetAvgDuration(t.Id))).FontSize(12);
								}
							});
						});
					page.Footer()
						.AlignCenter()
						.Text(x => {
							x.Span("Page ");
							x.CurrentPageNumber();
						});
				});
			});
			document.GeneratePdf(Path.Combine(ConfigManager.GetConfig().ReportLocation, "summarized.pdf"));
			logger.LogInformation($"Created summarized report. {DateTime.UtcNow}");
		}
	}
}