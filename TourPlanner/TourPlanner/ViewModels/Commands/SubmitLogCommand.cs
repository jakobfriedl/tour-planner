﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class SubmitLogCommand : BaseCommand
    {
        public LogDialogViewModel LogDialogViewModel { get; }
        public LogListViewModel LogListViewModel { get; }
        public bool IsUpdate { get; }

        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private int _totalTime;

        public SubmitLogCommand(LogDialogViewModel logDialogViewModel, LogListViewModel logListViewModel, bool isUpdate) {
	        LogDialogViewModel = logDialogViewModel;
	        LogListViewModel = logListViewModel;
            IsUpdate = isUpdate;
        }

		/// <summary>
		/// Validate Input-Fields
		/// </summary>
		/// <returns>True if all input fields are filled out correctly</returns>
        public override bool CanExecute(object? parameter) {
	        try {
		        _startDateTime = Convert.ToDateTime(LogDialogViewModel.LogDialogStartDateTime);
		        _endDateTime = Convert.ToDateTime(LogDialogViewModel.LogDialogEndDateTime);
		        _totalTime = (int)(_endDateTime - _startDateTime).TotalSeconds;
	        } catch (FormatException) {
		        return false; 
	        }

	        return !string.IsNullOrEmpty(LogDialogViewModel.LogDialogStartDateTime) &&
	               !string.IsNullOrEmpty(LogDialogViewModel.LogDialogEndDateTime) &&
	               !string.IsNullOrEmpty(LogDialogViewModel.LogDialogComment) &&
	               LogDialogViewModel.LogDialogDifficulty >= 0 && LogDialogViewModel.LogDialogDifficulty <= 10 &&
	               LogDialogViewModel.LogDialogRating >= 0 && LogDialogViewModel.LogDialogRating <= 10 &&
	               _totalTime > 0 && // EndDate is after StartDate
	               base.CanExecute(parameter);
        }

        public override void Execute(object? parameter) {
	        var log = new Log(LogDialogViewModel.SelectedTour.Id,
				_startDateTime,
		        _totalTime,
		        LogDialogViewModel.LogDialogComment,
		        LogDialogViewModel.LogDialogDifficulty,
		        LogDialogViewModel.LogDialogRating);

	        if (!IsUpdate) {
				log = LogDialogViewModel.GetCreatedLog(log); 
				LogListViewModel.AddLog(log);
	        }

	        LogDialogViewModel.CloseAction(); 
        }
    }
}