using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CourseProject.Models
{
    public class PlayerStats : INotifyPropertyChanged
    {
        private int _intelligence;
        private int _charisma;
        private int _strength;
        private int _money;
        private int _reputation;
        private int _mysteryPoints;

        public int Intelligence
        {
            get => _intelligence;
            set { _intelligence = value; OnPropertyChanged(); }
        }

        public int Charisma
        {
            get => _charisma;
            set { _charisma = value; OnPropertyChanged(); }
        }

        public int Strength
        {
            get => _strength;
            set { _strength = value; OnPropertyChanged(); }
        }

        public int Money
        {
            get => _money;
            set { _money = value; OnPropertyChanged(); }
        }

        public int Reputation
        {
            get => _reputation;
            set { _reputation = value; OnPropertyChanged(); }
        }

        public int MysteryPoints
        {
            get => _mysteryPoints;
            set { _mysteryPoints = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}