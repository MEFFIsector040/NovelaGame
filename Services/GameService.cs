using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CourseProject.Services
{
    public class GameService
    {
        private Dictionary<int, Scene> _scenes;
        private GameState _gameState;
        private PlayerStats _playerStats;

        public event Action<Scene> SceneChanged;
        public event Action<string> MessageAdded;

        public GameService()
        {
            _scenes = new Dictionary<int, Scene>();
            _gameState = new GameState();
            _playerStats = new PlayerStats
            {
                Intelligence = 50,
                Charisma = 50,
                Strength = 50,
                Money = 100,
                Reputation = 50,
                MysteryPoints = 0
            };

            InitializeAllScenes();
        }

        private void InitializeAllScenes()
        {
            CreateChapter1();
            CreateChapter2();
            CreateChapter3();
            CreateChapter4();
            CreateEndings();
        }

        private void CreateChapter1()
        {
            var scene1 = new Scene
            {
                Id = 1,
                Title = "Пробуждение",
                Description = "Вы просыпаетесь в незнакомой комнате отеля. Голова раскалывается от боли, " +
                            "а в памяти только обрывки воспоминаний о вчерашней вечеринке. " +
                            "На тумбочке рядом с кроватью лежит загадочное письмо с вашим именем.",
                ImagePath = "pack://application:,,,/Resources/Images/scene1.jpg"
            };
            scene1.Choices.Add(new Choice { Text = "Прочитать письмо", NextSceneId = 2 });
            scene1.Choices.Add(new Choice { Text = "Осмотреть комнату", NextSceneId = 3 });
            scene1.Choices.Add(new Choice { Text = "Попытаться вспомнить", NextSceneId = 4 });
            _scenes.Add(1, scene1);

            var scene2 = new Scene
            {
                Id = 2,
                Title = "Загадочное письмо",
                Description = "В письме написано: 'Если ты это читаешь, значит, всё пошло не по плану. " +
                            "Ты должен найти чёрный чемодан в номере 307. В нём ответы на все вопросы. " +
                            "Остерегайся человека в коричневом пальто. -А.'",
                ImagePath = "pack://application:,,,/Resources/Images/letter.jpg"
            };
            scene2.Choices.Add(new Choice { Text = "Отправиться в номер 307", NextSceneId = 5 });
            scene2.Choices.Add(new Choice { Text = "Вызвать полицию", NextSceneId = 6 });
            scene2.Choices.Add(new Choice { Text = "Игнорировать письмо", NextSceneId = 7 });
            _scenes.Add(2, scene2);

            var scene3 = new Scene
            {
                Id = 3,
                Title = "Осмотр комнаты",
                Description = "Вы медленно окидывете взглядом комнату... " +
                    "Всё как в тумане, вам все незнакомо",
                ImagePath = "pack://application:,,,/Resources/Images/scene1.jpg",
            };
            scene3.Choices.Add(new Choice { Text = "Прочитать письмо", NextSceneId = 2 });
            scene3.Choices.Add(new Choice { Text = "Попытаться вспомнить", NextSceneId = 4 });
            _scenes.Add(3, scene3);

            var scene4 = new Scene
            {
                Id = 4,
                Title = "Попытка вспомнить произошедшее",
                Description = "Вы напрягаете свою голову, чтобы вспомнить хоть одну деталь. "+
                    "Всё безуспешно...",
                ImagePath = "pack://application:,,,/Resources/Images/scene1.jpg",
            };
            scene4.Choices.Add(new Choice { Text = "Прочитать письмо", NextSceneId = 2 });
            _scenes.Add(4, scene4);

            var scene5 = new Scene
            {
                Id = 5,
                Title = "Вы идетё к комнате номер 307",
                Description = "Вы проходите по странному коридору. " +
                    "Cвет присутствовал, но темень как будто преобладала над светом.",
                ImagePath = "pack://application:,,,/Resources/Images/scene1.jpg",
            };
            scene5.Choices.Add(new Choice { Text = "Подойти к комнате 307", NextSceneId = 8 });
            _scenes.Add(5, scene5);

            var scene6 = new Scene
            {
                Id = 6,
                Title = "Звонок полиции",
                Description = "Вы передали информация в полицию. " +
                        "Отправляйтесь в номер 307",
                ImagePath = "pack://application:,,,/Resources/Images/letter.jpg"
            };
            scene6.Choices.Add(new Choice { Text = "Отправиться в номер 307", NextSceneId = 5 });
            _scenes.Add(6, scene6);

            var scene7 = new Scene
            {
                Id = 7,
                Title = "Вы проигнорировали письмо",
                Description = "Вы решаете пока не торопить события.",
                ImagePath = "pack://application:,,,/Resources/Images/scene1.jpg",
            };
            scene7.Choices.Add(new Choice { Text = "Прочитать письмо", NextSceneId = 2 });
            scene7.Choices.Add(new Choice { Text = "Попытаться вспомнить", NextSceneId = 4 });
            _scenes.Add(7, scene7);

            var scene8 = new Scene
            {
                Id = 8,
                Title = "Комната номер 307",
                Description = "Стоит, наконец, разобратьсь" + "Что же здесь происходит на самом деле",
                ImagePath = "pack://application:,,,/Resources/Images/door.jpg",
            };
            scene8.Choices.Add(new Choice { Text = "Постучаться", NextSceneId = 9 });
            scene8.Choices.Add(new Choice { Text = "Попробовать войти без стука", NextSceneId = 10});
            _scenes.Add(8, scene8);

            var scene9 = new Scene
            {
                Id = 9,
                Title = "Стук в дверь",
                Description = "По всем манерам приличия, вы стучите раз... два... три... " + "Ответа не последовало, нужно что-то придумать. ",
                ImagePath = "pack://application:,,,/Resources/Images/door.jpg",
            };
            scene9.Choices.Add(new Choice { Text = "Найти администратора", NextSceneId = 10 });
            _scenes.Add(9, scene9);
        }

        private void CreateChapter2()
        {
            for (int i = 11; i <= 20; i++)
            {
                var scene = new Scene
                {
                    Id = i,
                    Title = $"Расследование - Часть {i - 10}",
                    Description = GetChapter2Description(i),
                    ImagePath = $"pack://application:,,,/Resources/Images/scene{i}.jpg"
                };

                if (i == 11)
                {
                    scene.Choices.Add(new Choice
                    {
                        Text = "Убедить администратора показать записи",
                        NextSceneId = 12,
                        RequiredStat = "Charisma",
                        RequiredValue = 60
                    });
                    scene.Choices.Add(new Choice
                    {
                        Text = "Подкупить администратора",
                        NextSceneId = 13,
                        RequiredStat = "Money",
                        RequiredValue = 200
                    });
                    scene.Choices.Add(new Choice
                    {
                        Text = "Пробраться в офис тайком",
                        NextSceneId = 14,
                        RequiredStat = "Strength",
                        RequiredValue = 40
                    });
                }
                else if (i == 15)
                {
                    scene.Choices.Add(new Choice
                    {
                        Text = "Довериться незнакомцу",
                        NextSceneId = 16,
                        Effects = new Dictionary<string, int> { { "Reputation", 10 }, { "MysteryPoints", 20 } }
                    });
                    scene.Choices.Add(new Choice
                    {
                        Text = "Атаковать незнакомца",
                        NextSceneId = 17,
                        Effects = new Dictionary<string, int> { { "Strength", -10 }, { "Reputation", -20 } }
                    });
                    scene.Choices.Add(new Choice
                    {
                        Text = "Попытаться сбежать",
                        NextSceneId = 18
                    });
                }
                else
                {
                    int nextId1 = i + 1;
                    int nextId2 = i + 2;
                    int nextId3 = i + 3;

                    scene.Choices.Add(new Choice { Text = "Вариант 1", NextSceneId = nextId1 });
                    scene.Choices.Add(new Choice { Text = "Вариант 2", NextSceneId = nextId2 });
                    scene.Choices.Add(new Choice { Text = "Вариант 3", NextSceneId = nextId3 });
                }

                _scenes.Add(i, scene);
            }
        }

        private void CreateChapter3()
        {
            _scenes.Add(21, CreateBranchingScene(21, "Перекрёсток судьбы",
                "Вы стоите перед выбором: довериться таинственному союзнику или " +
                "действовать в одиночку. От этого зависит вся дальнейшая история.",
                new List<Choice>
                {
                    new Choice { Text = "Довериться союзнику", NextSceneId = 22,
                               Effects = new Dictionary<string, int> { { "Charisma", 15 }, { "MysteryPoints", 25 } } },
                    new Choice { Text = "Действовать самостоятельно", NextSceneId = 23,
                               Effects = new Dictionary<string, int> { { "Strength", 20 }, { "Intelligence", 10 } } },
                    new Choice { Text = "Искать третий путь", NextSceneId = 24,
                               Effects = new Dictionary<string, int> { { "Intelligence", 30 }, { "Money", -50 } } }
                }));

            var scene25 = new Scene
            {
                Id = 25,
                Title = "Тайная встреча",
                Description = "Вы встречаетесь с информатором в заброшенном театре. " +
                            "Он готов рассказать правду, но требует доказательства вашей искренности.",
                ImagePath = "pack://application:,,,/Resources/Images/theater.jpg"
            };

            scene25.Choices.Add(new Choice
            {
                Text = "Предъявить найденные документы",
                NextSceneId = 26,
                RequiredStat = "Intelligence",
                RequiredValue = 70,
                Effects = new Dictionary<string, int> { { "MysteryPoints", 40 } }
            });

            scene25.Choices.Add(new Choice
            {
                Text = "Предложить деньги",
                NextSceneId = 27,
                RequiredStat = "Money",
                RequiredValue = 300,
                Effects = new Dictionary<string, int> { { "Money", -300 }, { "Reputation", -10 } }
            });

            scene25.Choices.Add(new Choice
            {
                Text = "Угрожать",
                NextSceneId = 28,
                RequiredStat = "Strength",
                RequiredValue = 80,
                Effects = new Dictionary<string, int> { { "Reputation", -30 }, { "Strength", -20 } }
            });

            _scenes.Add(25, scene25);

            for (int i = 26; i <= 30; i++)
            {
                _scenes.Add(i, CreateGenericScene(i, $"Поворотный момент {i - 25}",
                    "Каждый выбор приближает вас к разгадке... или к пропасти."));
            }
        }

        private void CreateChapter4()
        {
            _scenes.Add(31, CreateFinalDecisionScene(31, "Последняя надежда"));
            _scenes.Add(32, CreateFinalDecisionScene(32, "Цена правды"));
            _scenes.Add(33, CreateFinalDecisionScene(33, "Момент истины"));
        }

        private void CreateEndings()
        {
            _scenes.Add(41, CreateEndingScene(41, "Истина восторжествовала",
                "Вы раскрыли заговор и предали виновных правосудию. Правда стала известна всем. " +
                "Хотя путь был труден, вы можете спать спокойно, зная, что сделали правильный выбор.",
                "good_ending1.jpg"));

            _scenes.Add(42, CreateEndingScene(42, "Новая жизнь",
                "Вы решили оставить прошлое позади. Под новым именем, в новом городе, " +
                "вы начинаете жизнь с чистого листа. Иногда правда должна остаться скрытой...",
                "good_ending2.jpg"));

            _scenes.Add(43, CreateEndingScene(43, "Цена молчания",
                "Вы приняли деньги и сохранили молчание. Но совесть не даёт покоя. " +
                "Каждую ночь вам снится тот, кто пострадал из-за вашего выбора.",
                "bad_ending1.jpg"));

            _scenes.Add(44, CreateEndingScene(44, "Забвение",
                "Вы слишком глубоко погрузились в тайны и потеряли себя. " +
                "Теперь вы - часть того, с чем боролись. Исход неизвестен.",
                "bad_ending2.jpg"));

            _scenes.Add(45, CreateEndingScene(45, "Секретная концовка",
                "Вы обнаружили истину за истиной! На самом деле всё это было симуляцией. " +
                "Поздравляем, вы прошли игру на 100%!",
                "secret_ending.jpg"));
        }

        private Scene CreateGenericScene(int id, string title, string description)
        {
            return new Scene
            {
                Id = id,
                Title = title,
                Description = description,
                ImagePath = $"pack://application:,,,/Resources/Images/scene{id}.jpg",
                Choices = new List<Choice>
                {
                    new Choice { Text = "Продолжить", NextSceneId = id + 1 },
                    new Choice { Text = "Осмотреться", NextSceneId = id + 2 },
                    new Choice { Text = "Вернуться", NextSceneId = id - 1 }
                }
            };
        }

        private Scene CreateBranchingScene(int id, string title, string description, List<Choice> choices)
        {
            return new Scene
            {
                Id = id,
                Title = title,
                Description = description,
                ImagePath = $"pack://application:,,,/Resources/Images/branch{id}.jpg",
                Choices = choices
            };
        }

        private Scene CreateFinalDecisionScene(int id, string title)
        {
            var scene = new Scene
            {
                Id = id,
                Title = title,
                Description = GetFinalDescription(id),
                ImagePath = $"pack://application:,,,/Resources/Images/final{id}.jpg"
            };

            if (_playerStats.Intelligence >= 80)
            {
                scene.Choices.Add(new Choice { Text = "Использовать интеллект", NextSceneId = 41 });
            }
            if (_playerStats.Charisma >= 80)
            {
                scene.Choices.Add(new Choice { Text = "Убедить противника", NextSceneId = 42 });
            }
            if (_playerStats.Strength >= 80)
            {
                scene.Choices.Add(new Choice { Text = "Применить силу", NextSceneId = 43 });
            }
            if (_playerStats.Money >= 500)
            {
                scene.Choices.Add(new Choice { Text = "Купить свободу", NextSceneId = 44 });
            }
            if (_playerStats.MysteryPoints >= 100)
            {
                scene.Choices.Add(new Choice { Text = "Раскрыть все тайны", NextSceneId = 45 });
            }

            return scene;
        }

        private Scene CreateEndingScene(int id, string title, string description, string image)
        {
            return new Scene
            {
                Id = id,
                Title = title,
                Description = description + "\n\n" + GetEndingStats(),
                ImagePath = $"pack://application:,,,/Resources/Images/{image}",
                IsEnding = true,
                Choices = new List<Choice>
                {
                    new Choice { Text = "Начать заново", NextSceneId = 1 },
                    new Choice { Text = "Выйти из игры", NextSceneId = -1 }
                }
            };
        }

        private string GetChapter2Description(int sceneId)
        {
            switch (sceneId)
            {
                case 11:
                    return "Вы спускаетесь в лобби отеля. Администратор бросает на вас подозрительный взгляд.";
                case 12:
                    return "Записи показывают, что вчера в отель заселились несколько подозрительных личностей.";
                case 13:
                    return "В коридоре вы встречаете человека в коричневом пальто. Он что-то ищет.";
                default:
                    return "Вы продолжаете расследование...";
            }
        }

        private string GetFinalDescription(int sceneId)
        {
            switch (sceneId)
            {
                case 31:
                    return "Вы стоите перед главным антагонистом. От вашего выбора зависит всё.";
                case 32:
                    return "Правда оказалась страшнее, чем вы могли предположить. Что теперь делать?";
                case 33:
                    return "Все нити сходятся к вам. Последнее решение перед развязкой.";
                default:
                    return "Финальная битва...";
            }
        }

        private string GetEndingStats()
        {
            return $"=== СТАТИСТИКА ИГРЫ ===\n" +
                   $"Интеллект: {_playerStats.Intelligence}\n" +
                   $"Харизма: {_playerStats.Charisma}\n" +
                   $"Сила: {_playerStats.Strength}\n" +
                   $"Деньги: ${_playerStats.Money}\n" +
                   $"Репутация: {_playerStats.Reputation}\n" +
                   $"Тайные очки: {_playerStats.MysteryPoints}\n" +
                   $"Посещено сцен: {_gameState.VisitedScenes.Count}\n" +
                   $"=== =============== ===";
        }

        public Scene GetCurrentScene()
        {
            if (_scenes.ContainsKey(_gameState.CurrentSceneId))
            {
                return _scenes[_gameState.CurrentSceneId];
            }
            return _scenes[1];
        }

        public List<Choice> GetAvailableChoices()
        {
            var currentScene = GetCurrentScene();
            var availableChoices = new List<Choice>();

            foreach (var choice in currentScene.Choices)
            {
                if (IsChoiceAvailable(choice))
                {
                    availableChoices.Add(choice);
                }
            }

            return availableChoices;
        }

        private bool IsChoiceAvailable(Choice choice)
        {
            if (string.IsNullOrEmpty(choice.RequiredStat))
            {
                return true;
            }

            var property = _playerStats.GetType().GetProperty(choice.RequiredStat);
            if (property != null)
            {
                int value = (int)property.GetValue(_playerStats);
                return value >= choice.RequiredValue;
            }

            return false;
        }

        public void MakeChoice(Choice choice)
        {
            _gameState.SceneHistory.Push(_gameState.CurrentSceneId);

            if (!_gameState.VisitedScenes.Contains(_gameState.CurrentSceneId))
            {
                _gameState.VisitedScenes.Add(_gameState.CurrentSceneId);
            }

            if (choice.Effects != null)
            {
                foreach (var effect in choice.Effects)
                {
                    ApplyEffect(effect.Key, effect.Value);
                }
            }

            _gameState.CurrentSceneId = choice.NextSceneId;

            MessageAdded?.Invoke($"Вы выбрали: {choice.Text}");
            SceneChanged?.Invoke(GetCurrentScene());
        }

        private void ApplyEffect(string stat, int value)
        {
            var property = _playerStats.GetType().GetProperty(stat);
            if (property != null)
            {
                int currentValue = (int)property.GetValue(_playerStats);
                property.SetValue(_playerStats, Math.Max(0, currentValue + value));
            }
        }

        public bool CanGoBack()
        {
            return _gameState.SceneHistory.Count > 0;
        }

        public void GoBack()
        {
            if (_gameState.SceneHistory.Count > 0)
            {
                _gameState.CurrentSceneId = _gameState.SceneHistory.Pop();
                SceneChanged?.Invoke(GetCurrentScene());
            }
        }

        public void SaveGame(string fileName)
        {
            try
            {
                var directory = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var saveData = new SaveData
                {
                    GameState = _gameState,
                    PlayerStats = _playerStats
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                };

                string json = JsonSerializer.Serialize(saveData, options);
                File.WriteAllText(fileName, json);

                MessageAdded?.Invoke($"Игра сохранена: {fileName}");
            }
            catch (Exception ex)
            {
                MessageAdded?.Invoke($"Ошибка сохранения: {ex.Message}");
            }
        }

        public void LoadGame(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    string json = File.ReadAllText(fileName);

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        MessageAdded?.Invoke($"Файл сохранения пуст: {fileName}");
                        return;
                    }

                    var options = new JsonSerializerOptions
                    {
                        IncludeFields = true
                    };

                    var saveData = JsonSerializer.Deserialize<SaveData>(json, options);

                    if (saveData != null)
                    {
                        _gameState = saveData.GameState ?? new GameState();
                        _playerStats = saveData.PlayerStats ?? new PlayerStats
                        {
                            Intelligence = 50,
                            Charisma = 50,
                            Strength = 50,
                            Money = 100,
                            Reputation = 50,
                            MysteryPoints = 0
                        };

                        _gameState.SceneHistory = _gameState.SceneHistory ?? new Stack<int>();
                        _gameState.VisitedScenes = _gameState.VisitedScenes ?? new List<int>();
                        _gameState.UnlockedEndings = _gameState.UnlockedEndings ?? new Dictionary<string, bool>();
                        _gameState.Inventory = _gameState.Inventory ?? new List<string>();

                        SceneChanged?.Invoke(GetCurrentScene());
                        MessageAdded?.Invoke($"Игра загружена: {fileName}");
                    }
                    else
                    {
                        MessageAdded?.Invoke($"Не удалось загрузить игру: файл повреждён");
                        ResetGame();
                    }
                }
                catch (JsonException ex)
                {
                    MessageAdded?.Invoke($"Ошибка загрузки (JSON): {ex.Message}");
                    ResetGame();
                }
                catch (Exception ex)
                {
                    MessageAdded?.Invoke($"Ошибка загрузки: {ex.Message}");
                    ResetGame();
                }
            }
            else
            {
                MessageAdded?.Invoke($"Файл сохранения не найден: {fileName}");
            }
        }

        public PlayerStats GetPlayerStats() => _playerStats;
        public GameState GetGameState() => _gameState;

        public void ResetGame()
        {
            _gameState = new GameState();
            _playerStats = new PlayerStats
            {
                Intelligence = 50,
                Charisma = 50,
                Strength = 50,
                Money = 100,
                Reputation = 50,
                MysteryPoints = 0
            };

            SceneChanged?.Invoke(GetCurrentScene());
            MessageAdded?.Invoke("Новая игра начата!");
        }
    }

    public class SaveData
    {
        public GameState GameState { get; set; }
        public PlayerStats PlayerStats { get; set; }
    }
}