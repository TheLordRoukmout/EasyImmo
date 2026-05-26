using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.BLL.Services;
using ImmoApp.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ImmoAppUI.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        private readonly EventService _eventService;

        // Mois et année affichés
        private int _currentYear;
        private int _currentMonth;

        private string _monthLabel = "";
        public string MonthLabel
        {
            get => _monthLabel;
            set => SetProperty(ref _monthLabel, value);
        }

        // Les jours du calendrier
        private ObservableCollection<CalendarDay> _days = new();
        public ObservableCollection<CalendarDay> Days
        {
            get => _days;
            set => SetProperty(ref _days, value);
        }

        public CalendarViewModel()
        {
            _eventService = new EventService();
            _currentYear = DateTime.Today.Year;
            _currentMonth = DateTime.Today.Month;
            LoadCalendar();
        }

        public void GoToPreviousMonth()
        {
            _currentMonth--;
            if (_currentMonth < 1)
            {
                _currentMonth = 12;
                _currentYear--;
            }
            LoadCalendar();
        }

        public void GoToNextMonth()
        {
            _currentMonth++;
            if (_currentMonth > 12)
            {
                _currentMonth = 1;
                _currentYear++;
            }
            LoadCalendar();
        }

        private void LoadCalendar()
        {
            // Label du mois
            var date = new DateTime(_currentYear, _currentMonth, 1);
            MonthLabel = date.ToString("MMMM yyyy", new System.Globalization.CultureInfo("fr-FR"));

            // Récupérer les événements du mois
            var events = _eventService.GetEventsByMonth(_currentYear, _currentMonth);

            // Construire les jours du calendrier
            var days = new ObservableCollection<CalendarDay>();

            // Ajouter les jours vides avant le 1er du mois
            int firstDayOfWeek = (int)date.DayOfWeek;
            if (firstDayOfWeek == 0) firstDayOfWeek = 7; // Dimanche = 7
            for (int i = 1; i < firstDayOfWeek; i++)
                days.Add(new CalendarDay { IsEmpty = true });

            // Ajouter les jours du mois
            int daysInMonth = DateTime.DaysInMonth(_currentYear, _currentMonth);
            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = new DateTime(_currentYear, _currentMonth, day);
                var dayEvents = events
                    .Where(e => e.DateEvent.Date == currentDate)
                    .ToList();

                days.Add(new CalendarDay
                {
                    DayNumber = day,
                    Date = currentDate,
                    IsToday = currentDate == DateTime.Today,
                    IsEmpty = false,
                    Events = new ObservableCollection<Event>(dayEvents)
                });
            }

            Days = days;
        }
    }

    // Classe représentant un jour du calendrier
    public class CalendarDay
    {
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
        public bool IsToday { get; set; }
        public bool IsEmpty { get; set; }
        public ObservableCollection<Event> Events { get; set; } = new();
    }
}