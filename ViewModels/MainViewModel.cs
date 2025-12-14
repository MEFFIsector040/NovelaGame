using CourseProject.Models;
using CourseProject.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CourseProject.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService;
        private readonly SaveService _saveService;
        private Scene _currentScene;
        private ObservableCollection<Choice> _availableChoices;
        private string _statusMessage;
        private ObservableCollection<string> _messages;

        public MainViewModel()
        {
            _gameService = new GameService();
            _saveService = new SaveService();
            _messages = new ObservableCollection<string>();
            _availableChoices = new ObservableCollection<Choice>();

            InitializeCommands();
            SubscribeToEvents();

            CurrentScene = _gameService.GetCurrentScene();
            UpdateAvailableChoices();
            AddMessage("Добро пожаловать в игру 'Тайны отеля'!");
        }

        public Scene CurrentScene
        {
            get => _currentScene;
            set
            {
                _currentScene = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Choice> AvailableChoices
        {
            get => _availableChoices;
            set
            {
                _availableChoices = value;
                OnPropertyChanged();
            }
        }

        public PlayerStats PlayerStats => _gameService.GetPlayerStats();

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Messages => _messages;

        public ObservableCollection<string> SaveFiles
        {
            get
            {
                var files = _saveService.GetSaveFiles();
                return new ObservableCollection<string>(files);
            }
        }

        public ICommand SelectChoiceCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand NewGameCommand { get; private set; }
        public ICommand DeleteSaveCommand { get; private set; }

        private void InitializeCommands()
        {
            SelectChoiceCommand = new RelayCommand<Choice>(choice =>
            {
                if (choice.NextSceneId == -1)
                {
                    System.Windows.Application.Current.Shutdown();
                    return;
                }

                _gameService.MakeChoice(choice);
            });

            GoBackCommand = new RelayCommand(() =>
            {
                _gameService.GoBack();
            }, () => _gameService.CanGoBack());

            SaveCommand = new RelayCommand<string>(saveName =>
            {
                if (!string.IsNullOrWhiteSpace(saveName))
                {
                    _gameService.SaveGame($"Saves/{saveName}.json");
                    OnPropertyChanged(nameof(SaveFiles));
                }
            });

            LoadCommand = new RelayCommand<string>(saveName =>
            {
                if (!string.IsNullOrWhiteSpace(saveName))
                {
                    _gameService.LoadGame($"Saves/{saveName}.json");
                    OnPropertyChanged(nameof(PlayerStats));
                }
            });

            NewGameCommand = new RelayCommand(() =>
            {
                _gameService.ResetGame();
                _messages.Clear();
                AddMessage("Новая игра начата!");
            });

            DeleteSaveCommand = new RelayCommand<string>(saveName =>
            {
                if (!string.IsNullOrWhiteSpace(saveName))
                {
                    _saveService.DeleteSave(saveName);
                    OnPropertyChanged(nameof(SaveFiles));
                    AddMessage($"Сохранение '{saveName}' удалено");
                }
            });
        }

        private void SubscribeToEvents()
        {
            _gameService.SceneChanged += OnSceneChanged;
            _gameService.MessageAdded += OnMessageAdded;
        }

        private void OnSceneChanged(Scene scene)
        {
            CurrentScene = scene;
            UpdateAvailableChoices();
            UpdateStatusMessage();
            OnPropertyChanged(nameof(PlayerStats));
        }

        private void OnMessageAdded(string message)
        {
            AddMessage(message);
        }

        private void AddMessage(string message)
        {
            _messages.Insert(0, $"{DateTime.Now:HH:mm:ss} - {message}");
            if (_messages.Count > 20)
            {
                _messages.RemoveAt(_messages.Count - 1);
            }
        }

        private void UpdateAvailableChoices()
        {
            var choices = _gameService.GetAvailableChoices();
            AvailableChoices.Clear();
            foreach (var choice in choices)
            {
                AvailableChoices.Add(choice);
            }
        }

        private void UpdateStatusMessage()
        {
            var stats = PlayerStats;
            StatusMessage = $"Интеллект: {stats.Intelligence} | " +
                           $"Харизма: {stats.Charisma} | " +
                           $"Сила: {stats.Strength} | " +
                           $"Деньги: ${stats.Money} | " +
                           $"Репутация: {stats.Reputation} | " +
                           $"Тайны: {stats.MysteryPoints}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}