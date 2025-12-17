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
                Money = 1000, // У студента в начале есть немного денег
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
            CreateEndings();
        }

        private void CreateChapter1()
        {
            // Сцена 1: Начало
            var scene1 = new Scene
            {
                Id = 1,
                Title = "Начало истории",
                Description = "Ты - студент престижного вуза в Санкт-Петербурге. " +
                            "Поступил по квоте как сын ветерана из многодетной семьи. " +
                            "С тобой в один университет поступила твоя девушка Даша. " +
                            "Кажется, жизнь прекрасна...",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene1.Choices.Add(new Choice { Text = "Продолжить", NextSceneId = 2 });
            _scenes.Add(1, scene1);

            // Сцена 2: Переезд в общагу
            var scene2 = new Scene
            {
                Id = 2,
                Title = "Новый этап",
                Description = "Чтобы быть ближе к Даше, ты переехал в ту же общагу, где живет она. " +
                            "Родители не одобряют твое решение, но ты уверен - это того стоит.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene2.Choices.Add(new Choice
            {
                Text = "Позвонить родителям",
                NextSceneId = 3,
                Effects = new Dictionary<string, int> { { "Reputation", 5 } }
            });
            scene2.Choices.Add(new Choice
            {
                Text = "Идти к Даше",
                NextSceneId = 4,
                Effects = new Dictionary<string, int> { { "Charisma", 5 } }
            });
            _scenes.Add(2, scene2);

            // Сцена 3: Разговор с родителями
            var scene3 = new Scene
            {
                Id = 3,
                Title = "Давление родителей",
                Description = "Родители недовольны твоим решением. " +
                            "'Ты должен сосредоточиться на учебе, а не на отношениях!' - говорит отец. " +
                            "Мать волнуется, что ты забросишь учебу.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene3.Choices.Add(new Choice
            {
                Text = "Пообещать хорошо учиться",
                NextSceneId = 4,
                Effects = new Dictionary<string, int> { { "Intelligence", 10 }, { "Reputation", 5 } }
            });
            scene3.Choices.Add(new Choice
            {
                Text = "Настоять на своем",
                NextSceneId = 4,
                Effects = new Dictionary<string, int> { { "Strength", 5 }, { "Reputation", -10 } }
            });
            _scenes.Add(3, scene3);

            // Сцена 4: Первые дни в общаге
            var scene4 = new Scene
            {
                Id = 4,
                Title = "Новые знакомства",
                Description = "В общаге много интересных людей, но ты почти не обращаешь на них внимания. " +
                            "Все твои мысли заняты Дасей. Сосед по комнате приглашает тебя познакомиться с компанией.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene4.Choices.Add(new Choice
            {
                Text = "Отказаться, пойти к Даше",
                NextSceneId = 5,
                Effects = new Dictionary<string, int> { { "Reputation", -5 }, { "Charisma", -5 } }
            });
            scene4.Choices.Add(new Choice
            {
                Text = "Принять приглашение",
                NextSceneId = 6,
                Effects = new Dictionary<string, int> { { "Charisma", 10 }, { "Reputation", 10 } }
            });
            _scenes.Add(4, scene4);
        }

        private void CreateChapter2()
        {
            // Сцена 5: Зависимость от Даши
            var scene5 = new Scene
            {
                Id = 5,
                Title = "Тревожные звоночки",
                Description = "Ты проводишь все время с Дасей, забросив учебу и новые знакомства. " +
                            "Она начинает отдаляться, ссылаясь на учебу и новых друзей. " +
                            "Ты чувствуешь, что что-то не так...",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene5.Choices.Add(new Choice
            {
                Text = "Стараться быть еще ближе",
                NextSceneId = 7,
                Effects = new Dictionary<string, int> { { "Charisma", -10 }, { "Strength", -5 } }
            });
            scene5.Choices.Add(new Choice
            {
                Text = "Дать ей пространство",
                NextSceneId = 8,
                Effects = new Dictionary<string, int> { { "Intelligence", 5 } }
            });
            _scenes.Add(5, scene5);

            // Сцена 6: Новые друзья
            var scene6 = new Scene
            {
                Id = 6,
                Title = "Новая компания",
                Description = "Ты познакомился с интересными ребятами из общаги. " +
                            "Они учатся на разных факультетах, у каждого своя история. " +
                            "Похоже, они могли бы стать хорошими друзьями.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\company.jpg"
            };
            scene6.Choices.Add(new Choice
            {
                Text = "Продолжать общаться",
                NextSceneId = 8,
                Effects = new Dictionary<string, int> { { "Charisma", 15 }, { "Reputation", 10 } }
            });
            scene6.Choices.Add(new Choice
            {
                Text = "Вернуться к Даше",
                NextSceneId = 7,
                Effects = new Dictionary<string, int> { { "Charisma", -5 } }
            });
            _scenes.Add(6, scene6);

            // Сцена 7: Разрыв
            var scene7 = new Scene
            {
                Id = 7,
                Title = "Разрыв",
                Description = "Даша объявляет, что хочет расстаться. 'Ты слишком зависим от меня, " +
                            "у тебя нет своей жизни!' - говорит она. Ты чувствуешь, как мир рушится.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene7.Choices.Add(new Choice
            {
                Text = "Умолять ее остаться",
                NextSceneId = 9,
                Effects = new Dictionary<string, int> { { "Strength", -20 }, { "Reputation", -15 } }
            });
            scene7.Choices.Add(new Choice
            {
                Text = "Принять ее решение",
                NextSceneId = 10,
                Effects = new Dictionary<string, int> { { "Intelligence", 10 }, { "Strength", 5 } }
            });
            _scenes.Add(7, scene7);

            // Сцена 8: Поддержка друзей
            var scene8 = new Scene
            {
                Id = 8,
                Title = "Неожиданная поддержка",
                Description = "Новые друзья из общаги заметили, что у тебя проблемы. " +
                            "Они предлагают помощь и поддержку.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene8.Choices.Add(new Choice
            {
                Text = "Принять помощь",
                NextSceneId = 10,
                Effects = new Dictionary<string, int> { { "Charisma", 15 }, { "Strength", 10 } }
            });
            scene8.Choices.Add(new Choice
            {
                Text = "Отказаться, справляться самому",
                NextSceneId = 9,
                Effects = new Dictionary<string, int> { { "Strength", -10 } }
            });
            _scenes.Add(8, scene8);

            // Сцена 9: Падение на дно
            var scene9 = new Scene
            {
                Id = 9,
                Title = "Темные дни",
                Description = "Ты впадаешь в депрессию. Учеба заброшена, ты почти не выходишь из комнаты. " +
                            "Родители в ярости, друзья пытаются достучаться до тебя.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene9.Choices.Add(new Choice
            {
                Text = "Позвонить родителям",
                NextSceneId = 11,
                Effects = new Dictionary<string, int> { { "Reputation", -10 }, { "Strength", -5 } }
            });
            scene9.Choices.Add(new Choice
            {
                Text = "Позвонить друзьям",
                NextSceneId = 12,
                Effects = new Dictionary<string, int> { { "Charisma", 5 }, { "Strength", 5 } }
            });
            scene9.Choices.Add(new Choice
            {
                Text = "Остаться в одиночестве",
                NextSceneId = 13,
                Effects = new Dictionary<string, int> { { "Strength", -20 }, { "Intelligence", -10 } }
            });
            _scenes.Add(9, scene9);
        }

        private void CreateChapter3()
        {
            // Сцена 10: Начало восстановления
            var scene10 = new Scene
            {
                Id = 10,
                Title = "Первые шаги",
                Description = "С поддержкой друзей ты начинаешь потихоньку приходить в себя. " +
                            "Они заставляют тебя выходить из комнаты, возвращаться к учебе.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene10.Choices.Add(new Choice
            {
                Text = "Сосредоточиться на учебе",
                NextSceneId = 14,
                Effects = new Dictionary<string, int> { { "Intelligence", 20 }, { "Reputation", 10 } }
            });
            scene10.Choices.Add(new Choice
            {
                Text = "Проводить время с друзьями",
                NextSceneId = 15,
                Effects = new Dictionary<string, int> { { "Charisma", 15 }, { "Strength", 10 } }
            });
            _scenes.Add(10, scene10);

            // Сцена 11: Разговор с родителями (альтернативный путь)
            var scene11 = new Scene
            {
                Id = 11,
                Title = "Семейный разговор",
                Description = "Родители приехали в Петербург. Они серьезно обеспокоены твоим состоянием. " +
                            "'Мы хотим, чтобы ты перевелся в другой вуз поближе к дому' - говорит отец.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene11.Choices.Add(new Choice
            {
                Text = "Согласиться на переезд",
                NextSceneId = 41, // Плохая концовка
                Effects = new Dictionary<string, int> { { "Reputation", -20 }, { "Strength", -15 } }
            });
            scene11.Choices.Add(new Choice
            {
                Text = "Пообещать исправиться",
                NextSceneId = 14,
                Effects = new Dictionary<string, int> { { "Intelligence", 10 }, { "Reputation", 5 } }
            });
            _scenes.Add(11, scene11);

            // Сцена 12: Помощь друзей
            var scene12 = new Scene
            {
                Id = 12,
                Title = "Дружеская интервенция",
                Description = "Друзья буквально вытаскивают тебя из комнаты. " +
                            "Они устраивают вечер кино, прогулки по городу, помогают с учебой.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene12.Choices.Add(new Choice
            {
                Text = "Открыться друзьям",
                NextSceneId = 16,
                Effects = new Dictionary<string, int> { { "Charisma", 20 }, { "Strength", 15 } }
            });
            scene12.Choices.Add(new Choice
            {
                Text = "Держаться на расстоянии",
                NextSceneId = 17,
                Effects = new Dictionary<string, int> { { "Charisma", -5 } }
            });
            _scenes.Add(12, scene12);

            // Сцена 13: Полная изоляция (альтернативный путь)
            var scene13 = new Scene
            {
                Id = 13,
                Title = "Одиночество",
                Description = "Ты полностью отрезал себя от мира. Учеба провалена, " +
                            "друзья перестали звонить, родители в отчаянии.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene13.Choices.Add(new Choice
            {
                Text = "Позвонить кому-нибудь",
                NextSceneId = 16,
                Effects = new Dictionary<string, int> { { "Strength", 10 } }
            });
            scene13.Choices.Add(new Choice
            {
                Text = "Сдаться окончательно",
                NextSceneId = 42, // Плохая концовка
                Effects = new Dictionary<string, int> { { "Strength", -30 }, { "Intelligence", -20 } }
            });
            _scenes.Add(13, scene13);

            // Сцена 14: Возвращение к учебе
            var scene14 = new Scene
            {
                Id = 14,
                Title = "Учебный процесс",
                Description = "Ты возвращаешься к учебе. Преподаватели замечают твои старания. " +
                            "Оказывается, учеба может быть интересной, когда не думаешь постоянно о Даше.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene14.Choices.Add(new Choice
            {
                Text = "Углубиться в учебу",
                NextSceneId = 18,
                Effects = new Dictionary<string, int> { { "Intelligence", 25 }, { "Reputation", 15 } }
            });
            scene14.Choices.Add(new Choice
            {
                Text = "Найти баланс с общением",
                NextSceneId = 19,
                Effects = new Dictionary<string, int> { { "Intelligence", 15 }, { "Charisma", 15 } }
            });
            _scenes.Add(14, scene14);

            // Сцена 15: Новые увлечения
            var scene15 = new Scene
            {
                Id = 15,
                Title = "Открытия",
                Description = "Друзья знакомят тебя со своими хобби: кто-то занимается спортом, " +
                            "кто-то музыкой, кто-то программированием. Ты начинаешь находить себя.",
                ImagePath = "C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\history.jpg"
            };
            scene15.Choices.Add(new Choice
            {
                Text = "Попробовать спорт",
                NextSceneId = 20,
                Effects = new Dictionary<string, int> { { "Strength", 20 } }
            });
            scene15.Choices.Add(new Choice
            {
                Text = "Заняться программированием",
                NextSceneId = 21,
                Effects = new Dictionary<string, int> { { "Intelligence", 25 } }
            });
            scene15.Choices.Add(new Choice
            {
                Text = "Попробовать музыку",
                NextSceneId = 22,
                Effects = new Dictionary<string, int> { { "Charisma", 20 } }
            });
            _scenes.Add(15, scene15);

            // Сцена 16-22: Развитие выбранного пути (объединяем для краткости)
            for (int i = 16; i <= 22; i++)
            {
                string title = "";
                string description = "";
                Dictionary<string, int> effects = new Dictionary<string, int>();

                if (i == 16)
                {
                    title = "Искренность";
                    description = "Ты открылся друзьям, и это сблизило вас еще больше. " +
                                "Оказалось, у каждого есть свои трудности и переживания.";
                    effects = new Dictionary<string, int> { { "Charisma", 25 }, { "Reputation", 20 } };
                }
                else if (i == 17)
                {
                    title = "Осторожность";
                    description = "Ты держишь дистанцию, но друзья все равно рядом. " +
                                "Постепенно ты начинаешь доверять им больше.";
                    effects = new Dictionary<string, int> { { "Intelligence", 15 }, { "Charisma", 10 } };
                }
                else if (i == 18)
                {
                    title = "Успехи в учебе";
                    description = "Ты становишься одним из лучших студентов. " +
                                "Преподаватели предлагают участие в научной работе.";
                    effects = new Dictionary<string, int> { { "Intelligence", 30 }, { "Reputation", 25 } };
                }
                else if (i == 19)
                {
                    title = "Баланс";
                    description = "Ты находишь гармонию между учебой и общением. " +
                                "Жизнь начинает налаживаться.";
                    effects = new Dictionary<string, int> { { "Intelligence", 20 }, { "Charisma", 20 }, { "Strength", 10 } };
                }
                else if (i == 20)
                {
                    title = "Спортивные достижения";
                    description = "Спорт помогает справиться со стрессом. " +
                                "Ты становишься сильнее физически и морально.";
                    effects = new Dictionary<string, int> { { "Strength", 30 }, { "Charisma", 10 } };
                }
                else if (i == 21)
                {
                    title = "Программирование";
                    description = "Ты открываешь для себя мир IT. " +
                                "Первые успешные проекты дают уверенность в себе.";
                    effects = new Dictionary<string, int> { { "Intelligence", 35 } };
                }
                else if (i == 22)
                {
                    title = "Музыкальная душа";
                    description = "Музыка становится твоим способом выражения эмоций. " +
                                "Ты даже начинаешь выступать на студенческих мероприятиях.";
                    effects = new Dictionary<string, int> { { "Charisma", 30 }, { "Reputation", 15 } };
                }

                var scene = new Scene
                {
                    Id = i,
                    Title = title,
                    Description = description,
                    ImagePath = $"C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\scene11.jpg"
                };
                scene.Choices.Add(new Choice
                {
                    Text = "Продолжить свой путь",
                    NextSceneId = 30,
                    Effects = effects
                });
                _scenes.Add(i, scene);
            }
        }

        private void CreateEndings()
        {
            // Хорошие концовки
            _scenes.Add(30, CreateEndingScene(30, "Новая жизнь",
                "Прошел год. Ты успешно учишься, у тебя настоящие друзья, " +
                "ты нашел свои увлечения. Иногда вспоминаешь Дашу, но уже без боли. " +
                "Ты понял, что зависимость от кого-то - это тупик, а настоящая свобода " +
                "и счастье приходят, когда ты ценишь себя и строить свою жизнь.",
                "history.jpg",
                new Dictionary<string, int> { { "Charisma", 50 }, { "Intelligence", 50 }, { "Strength", 50 } }));

            _scenes.Add(31, CreateEndingScene(31, "Прощение и рост",
                "Ты встретил Дашу случайно на улице. Вы поговорили, и ты понял, " +
                "что благодарен ей за тот тяжелый опыт. Он сделал тебя сильнее. " +
                "Ты научился ценить себя и не зависеть от других. Теперь у тебя " +
                "есть планы на будущее и люди, которые действительно тебя ценят.",
                "history.jpg",
                new Dictionary<string, int> { { "Intelligence", 60 }, { "Charisma", 40 } }));

            // Плохие концовки
            _scenes.Add(41, CreateEndingScene(41, "Потерянный шанс",
                "Ты вернулся домой, поддавшись давлению родителей. Учеба в местном " +
                "вузе не приносит удовольствия. Ты постоянно думаешь о том, " +
                "как могла сложиться жизнь в Петербурге. Ощущение, что ты сдался " +
                "и упустил свой шанс на независимость и рост.",
                "history.jpg",
                new Dictionary<string, int> { { "Strength", -30 }, { "Reputation", -20 } }));

            _scenes.Add(42, CreateEndingScene(42, "Полная потеря контроля",
                "Ты окончательно погрузился в депрессию. Отчислили из университета, " +
                "друзья отвернулись, родители в отчаянии. Ты стал тем, от кого " +
                "предостерегали в начале пути - человеком, потерявшим себя из-за " +
                "неразделенных чувств и неумения просить о помощи.",
                "history.jpg",
                new Dictionary<string, int> { { "Strength", -50 }, { "Intelligence", -40 }, { "Charisma", -50 } }));

            // Секретная концовка (если все статы высокие)
            _scenes.Add(43, CreateEndingScene(43, "Истинное призвание",
                "Прошло несколько лет. Ты не только закончил университет с отличием, " +
                "но и основал свой стартап с друзьями из общаги. Ты научился ценить " +
                "себя, свои чувства и окружающих людей. Трудности сделали тебя мудрее " +
                "и сильнее. Теперь ты точно знаешь: самые ценные сокровища - это " +
                "друзья, семья и умение делиться своими эмоциями.",
                "history.jpg",
                new Dictionary<string, int> { { "Intelligence", 100 }, { "Charisma", 100 }, { "Strength", 100 } }));
        }

        private Scene CreateEndingScene(int id, string title, string description,
                                      string image, Dictionary<string, int> statChanges = null)
        {
            if (statChanges != null)
            {
                foreach (var change in statChanges)
                {
                    ApplyEffect(change.Key, change.Value);
                }
            }

            return new Scene
            {
                Id = id,
                Title = title,
                Description = description,
                ImagePath = $"C:\\Users\\meffistoped\\source\\repos\\CourseProject\\Resources\\Images\\{image}",
                IsEnding = true,
                Choices = new List<Choice>
                {
                    new Choice { Text = "Начать заново", NextSceneId = 1 },
                    new Choice { Text = "Выйти из игры", NextSceneId = -1 }
                }
            };
        }

        //private string GetEndingStats()
        //{
        //    return $"=== СТАТИСТИКА ИГРЫ ===\n" +
        //           $"Интеллект: {_playerStats.Intelligence}\n" +
        //           $"Харизма: {_playerStats.Charisma}\n" +
        //           $"Сила: {_playerStats.Strength}\n" +
        //           $"Деньги: ${_playerStats.Money}\n" +
        //           $"Репутация: {_playerStats.Reputation}\n" +
        //           $"Посещено сцен: {_gameState.VisitedScenes.Count}\n" +
        //           $"=== =============== ===";
        //}

        // Остальные методы остаются без изменений
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
                property.SetValue(_playerStats, Math.Max(0, Math.Min(100, currentValue + value)));
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
                            Money = 1000,
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
                Money = 1000,
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