using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;

#region Библиотеки классов
using ClientLibrary;
using ClientLibrary.Service;
using UsersLibrary;
using HistoryLibrary;
using DialogLibrary.DialogService;
using DialogLibrary;
using FileLibrary;
using MVVMWpfLibraryBase;
#endregion

namespace Home_Work_15
{
    /// <summary>
    /// Класс связывает интерфейс и логику из подключенных библиотек
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        #region Поля
        private DataList dataList = new DataList(); // Списки БД
        private ClientAbstract selectedClient; // Выбранный клиент
        private IUsers currentUser; // Текущий пользователь
        private decimal txtReplenishDeposit; // Сумма для пополнения депозита
        private decimal txtTransferToAccount; // Сумма для перевода на счёт
        private decimal txtReplenishAccount; // Сумма для пополнения счёта
        private decimal txtWithdrawAccount; // Сумма для снятие счёта
        private decimal txtTransferToDeposit; // Сумма перевода со счёта на депозит
        private decimal txtTransferToClient; // Сумма для перевода другому клиенту
        private string txtCurrentUser; // Статус текущего пользователя
        #endregion

        #region Сервисы
        private ClientBaseService serviceClient = new ClientBaseService(); // Работа с данными клиента
        private DialogAddClientService dialogAddClientService = new DialogAddClientService(); // Добавление клиента
        private DialogTransferClientService dialogTransfer = new DialogTransferClientService(); // Перевод клиенту
        private DialogFileService dialogFileService = new DialogFileService(); // Окна для работы с файлами
        private DialogHelpService dialogHelpService = new DialogHelpService(); // Окно с короткой инструкцией
        private AccountService accountService = new AccountService(); // Работа со счётом клиента
        private DepositService depositService = new DepositService(); // Работа с депозитным счётом клиента
        private DialogAuthorizationService dialogAuthorizationService = new DialogAuthorizationService(); // Окно авторизации
        private DialogHistoryService dialogHistoryService = new DialogHistoryService(); // Окно с историей
        private IFile fileService; // Работа с файлами
        #endregion

        #region Свойства
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public ClientAbstract SelectedClient
        {
            get => selectedClient;
            set => Set(ref selectedClient, value);
        }

        /// <summary>
        /// Список клиентов
        /// </summary>
        public ObservableCollection<ClientAbstract> ClientsList { get; set; }

        /// <summary>
        /// Список истории
        /// </summary>
        public ObservableCollection<History> HistoryList { get; set; }

        /// <summary>
        /// Сумма для пополнения депозита
        /// </summary>
        public decimal TxtReplenishDeposit
        {
            get => txtReplenishDeposit;
            set => Set(ref txtReplenishDeposit, value);
        }

        /// <summary>
        /// Сумма для перевода на счёт с депозита
        /// </summary>
        public decimal TxtTransferToAccount
        {
            get => txtTransferToAccount;
            set => Set(ref txtTransferToAccount, value);
        }

        /// <summary>
        /// Сумма для пополнения счёта
        /// </summary>
        public decimal TxtReplenishAccount
        {
            get => txtReplenishAccount;
            set => Set(ref txtReplenishAccount, value);
        }

        /// <summary>
        /// Сумма для снятие счёта
        /// </summary>
        public decimal TxtWithdrawAccount
        {
            get => txtWithdrawAccount;
            set => Set(ref txtWithdrawAccount, value);
        }

        /// <summary>
        /// Сумма перевода со счёта на депозит
        /// </summary>
        public decimal TxtTransferToDeposit
        {
            get => txtTransferToDeposit;
            set => Set(ref txtTransferToDeposit, value);
        }

        /// <summary>
        /// Сумма для перевода другому клиенту
        /// </summary>
        public decimal TxtTransferToClient
        {
            get => txtTransferToClient;
            set => Set(ref txtTransferToClient, value);
        }

        /// <summary>
        /// Статус текущего пользователя
        /// </summary>
        public string TxtCurrentUser
        {
            get => $"Текущий пользователь - {txtCurrentUser}";
            set => Set(ref txtCurrentUser, value);
        }
        #endregion

        #region Конструктор
        public MainWindowVM()
        {
            ClientsList = new ObservableCollection<ClientAbstract>();
            HistoryList = new ObservableCollection<History>();
            fileService = new FileService();
            accountService.AccountAddHistory += AccountAddLog;
            depositService.DepositAddHistory += AccountAddLog;
            serviceClient.BaseAddHistory += AccountAddLog;
            serviceClient.Display += PrintMessage;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Метод обнуляет значения TXT
        /// </summary>
        private void ResetValues()
        {
            TxtReplenishAccount = 0;
            TxtTransferToDeposit = 0;
            TxtTransferToClient = 0;
            TxtWithdrawAccount = 0;
            TxtTransferToAccount = 0;
            TxtReplenishDeposit = 0;
        }

        /// <summary>
        /// Присвоить значения листам в DataList
        /// </summary>
        private void AssignValuesData(bool flag)
        {
            if (flag == true)
            {
                dataList.clientsList = ClientsList;
                dataList.historyList = HistoryList;
            }

            else
            {
                ClientsList.Clear();
                foreach (var c in dataList.clientsList)
                    ClientsList.Add(c);

                HistoryList.Clear();
                //foreach (var h in dataList.historyList)
                //    HistoryList.Add(h);
                HistoryList = dataList.historyList;
            }
        }

        /// <summary>
        /// Обработчик события AccountAddHistory и DepositAddHistory
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oper"></param>
        /// <param name="sum"></param>
        /// <param name="client"></param>
        private void AccountAddLog(IUsers user, string oper, decimal sum, ClientAbstract client)
        {
            History log = new History(user, oper, sum, client);
            HistoryList.Add(log);
        }

        /// <summary>
        /// Обработчик события Display
        /// </summary>
        /// <param name="Mes"></param>
        private void PrintMessage (string Mes)
        {
            MessageBox.Show(Mes);
        }

        #endregion

        #region Комманды - общие комманды
        private RelayCommand generatorBaseCommand; // Комманда создания БД
        private RelayCommand addClientCommand; // Комманда добавления клиента
        private RelayCommand deleteClientCommand; // Команда удаление выбранного клиента
        private RelayCommand openFileCommand; // Комманда открыть файл
        private RelayCommand saveFileCommand; // Комманда сохранить файл
        private RelayCommand openHelpCommand; // Команда открывает окно помощи
        private RelayCommand selectUserCommand; // Комманда открывает окно авторизации
        private RelayCommand openHistoryCommand; // Комманда открывает окно с историей


        /// <summary>
        /// Комманда создания БД
        /// </summary>
        public RelayCommand GeneratorBaseCommand
        {
            get
            {
                return generatorBaseCommand ?? (generatorBaseCommand = new RelayCommand(obj =>
                {
                    ClientsList.Clear();
                    ObservableCollection<ClientAbstract> cli = serviceClient.GeneratorBase();

                    for (int i = 0; i < cli.Count; i++)
                        ClientsList.Add(cli[i]);
                }));
            }
        }

        /// <summary>
        /// Комманда добавления клиента
        /// </summary>
        public RelayCommand AddClientCommand
        {
            get
            {
                return addClientCommand ?? (addClientCommand = new RelayCommand(obj =>
                {
                    if (dialogAddClientService.OpenAddClientDialog() == true)
                    {
                        ClientsList = serviceClient.AddClient(ClientsList, dialogAddClientService.client, currentUser);
                    }
                }));
            }
        }

        /// <summary>
        /// Комманда удаления текущего клиента
        /// </summary>
        public RelayCommand DeleteClientCommand
        {
            get
            {
                return deleteClientCommand ?? (deleteClientCommand = new RelayCommand(obj =>
                {
                    ClientAbstract client = obj as ClientAbstract;
                    ClientsList = serviceClient.DeleteClient(ClientsList, client, currentUser);
                },
                obj => ClientsList.Count > 0 && SelectedClient != null));
            }
        }

        /// <summary>
        /// Команда открыть файл с БД
        /// </summary>
        public RelayCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ?? (openFileCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialogFileService.OpenFileDialog() == true)
                        {
                            dataList = fileService.OpenFile(dialogFileService.FilePath);
                            AssignValuesData(false);

                            MessageBox.Show("Файл успешно загружен");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка");
                    }
                }));
            }
        }

        /// <summary>
        /// Комманда сохранить файл с БД
        /// </summary>
        public RelayCommand SaveFileCommand
        {
            get
            {
                return saveFileCommand ?? (saveFileCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialogFileService.SaveFileDialog() == true)
                        {
                            AssignValuesData(true);
                            fileService.SaveFile(dialogFileService.FilePath, dataList);
                            MessageBox.Show("Файл сохранён");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка");
                    }
                },
                obj => ClientsList.Count > 0));
            }
        }

        /// <summary>
        /// Команда открывает окно помощи
        /// </summary>
        public RelayCommand OpenHelpCommand
        {
            get
            {
                return openHelpCommand ?? (openHelpCommand = new RelayCommand(obj =>
                {
                    dialogHelpService.OpenDialogHelp();
                }));
            }
        }

        /// <summary>
        /// Комманда открывает окно авторизации
        /// </summary>
        public RelayCommand SelectUserCommand
        {
            get
            {
                return selectUserCommand ?? (selectUserCommand = new RelayCommand(obj =>
                {
                    currentUser = null;
                    while (currentUser == null)
                    {
                        try
                        {
                            if (dialogAuthorizationService.OpenAuthorizationDialog() == true)
                            {
                                currentUser = dialogAuthorizationService.SelectedUser;
                                TxtCurrentUser = currentUser.Name;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка");
                        }

                        if (currentUser == null) MessageBox.Show("Нужно выбрать пользователя");
                    }
                }));
            }
        }

        /// <summary>
        /// Комманда открывает окно с историей
        /// </summary>
        public RelayCommand OpenHistoryCommand
        {
            get
            {
                return openHistoryCommand ?? (openHistoryCommand = new RelayCommand(obj =>
                {
                    dialogHistoryService.OpenHistoryWindow(HistoryList);
                }));
            }
        }
        #endregion

        #region Комманды - работа с депозитом

        private RelayCommand openDepositAccountCommand; // Добавление депозитного счёта
        private RelayCommand closeDepositAccountComand; // Закрытие депозитного счёта
        private RelayCommand replenishDepositCommand; // Пополнить депозит
        private RelayCommand transferToAccountCommand; // Перевод с депозита на счёт

        /// <summary>
        /// Открыть клиенту депозитный счёт
        /// </summary>
        public RelayCommand OpenDepositAccountCommand
        {
            get
            {
                return openDepositAccountCommand ?? (openDepositAccountCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        SelectedClient = depositService.OpenDepositAccount(SelectedClient, currentUser);
                    }
                    catch (ClientException e) // Обработка сгенерированного исключения
                    {
                        PrintMessage(e.Message);
                    }
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return true; // SelectedClient.DepositAccount == 0;
                }));
            }
        }

        /// <summary>
        /// Закрыть депозит клиенту
        /// </summary>
        public RelayCommand CloseDepositAccountComand
        {
            get
            {
                return closeDepositAccountComand ?? (closeDepositAccountComand = new RelayCommand(obj =>
                {
                    try
                    {
                        SelectedClient = depositService.CloseDepositAccount(SelectedClient, currentUser);
                    }
                    catch (ClientException e) // Обработка сгенерированного исключения
                    {
                        PrintMessage(e.Message);
                    }
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return true; // SelectedClient.DepositAccount > 0;
                }));
            }
        }

        /// <summary>
        /// Пополнить депозит
        /// </summary>
        public RelayCommand ReplenishDepositCommand
        {
            get
            {
                return replenishDepositCommand ?? (replenishDepositCommand = new RelayCommand(obj =>
                {
                    SelectedClient = depositService.ReplenishDeposit(SelectedClient, TxtReplenishDeposit, currentUser);
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return SelectedClient.DepositAccount > 0 && TxtReplenishDeposit > -1;
                }));
            }
        }

        /// <summary>
        /// Перевести средства с депозита а счёт
        /// </summary>
        public RelayCommand TransferToAccountCommand
        {
            get
            {
                return transferToAccountCommand ?? (transferToAccountCommand = new RelayCommand(obj =>
                {
                    SelectedClient = depositService.TransferToAccount(SelectedClient, TxtTransferToAccount, currentUser);
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return (SelectedClient.DepositAccount > txtTransferToAccount && SelectedClient.Account > 0
                        && TxtTransferToAccount > -1);
                }));
            }
        }
        #endregion

        #region Комманды - работа со счётом
        private RelayCommand openAccountCommand; // Открыть счёт клиенту
        private RelayCommand closeAccountCommand; // Закрыть счёт клиенту
        private RelayCommand replenishAccountCommand; // Пополнить счёт клиента
        private RelayCommand withdrawAccountCommand; // Снять средства со счёта
        private RelayCommand transferToDepositCommand; // Перевести средства со счёта на депозит
        private RelayCommand transferToClientCommand; // Перевести средства другому клиенту

        /// <summary>
        /// Открыть счёт клиенту
        /// </summary>
        public RelayCommand OpenAccountCommand
        {
            get
            {
                return openAccountCommand ?? (openAccountCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        SelectedClient = accountService.OpenAccount(SelectedClient, currentUser);
                    }
                    catch (ClientException e) // Обработка сгенерированного исключения
                    {
                        PrintMessage(e.Message);
                    }
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return true; // SelectedClient.Account == 0;
                }));
            }
        }

        /// <summary>
        /// Закрыть счёт клиента
        /// </summary>
        public RelayCommand CloseAccountCommand
        {
            get
            {
                return closeAccountCommand ?? (closeAccountCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        SelectedClient = accountService.CloseAccount(SelectedClient, currentUser);
                    }
                    catch (ClientException e) // Обработка сгенерируемого исключения
                    {
                        PrintMessage(e.Message);
                    }
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return true; // SelectedClient.Account > 0;
                }));
            }
        }

        /// <summary>
        /// Пополнить счёт клиента
        /// </summary>
        public RelayCommand ReplenishAccountCommand
        {
            get
            {
                return replenishAccountCommand ?? (replenishAccountCommand = new RelayCommand(obj =>
                {
                    SelectedClient = accountService.ReplenishAccount(SelectedClient, TxtReplenishAccount, currentUser);
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return SelectedClient.Account > 0 && TxtReplenishAccount > -1;
                }));
            }
        }

        /// <summary>
        /// Снять средства со счёта
        /// </summary>
        public RelayCommand WithdrawAccountCommand
        {
            get
            {
                return withdrawAccountCommand ?? (withdrawAccountCommand = new RelayCommand(obj =>
                {
                    SelectedClient = accountService.WithdrawAccount(SelectedClient, TxtWithdrawAccount, currentUser);
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return SelectedClient.Account > 0 && TxtWithdrawAccount < SelectedClient.Account
                        && TxtWithdrawAccount > -1;
                }));
            }
        }

        /// <summary>
        /// Перевести средства со счёта на депозит
        /// </summary>
        public RelayCommand TransferToDepositCommand
        {
            get
            {
                return transferToDepositCommand ?? (transferToDepositCommand = new RelayCommand(obj =>
                {
                    SelectedClient = accountService.TransferToDeposit(SelectedClient, TxtTransferToDeposit, currentUser);
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return SelectedClient.Account > TxtTransferToDeposit && SelectedClient.DepositAccount > 0
                        && TxtTransferToDeposit > -1;
                }));
            }
        }

        /// <summary>
        /// Перевод на счёт другого клиента
        /// </summary>
        public RelayCommand TransferToClientCommand
        {
            get
            {
                return transferToClientCommand ?? (transferToClientCommand = new RelayCommand(obj =>
                {
                    dialogTransfer.OpenTransferClientDialog(ClientsList, SelectedClient);

                    for (int i = 0; i < ClientsList.Count; i++)
                    {
                        if (ClientsList[i].Equals(dialogTransfer.clientTransfer))
                        {
                            ClientsList[i] = accountService.ReplenishAccount(ClientsList[i], TxtTransferToClient, currentUser);
                            SelectedClient = accountService.WithdrawAccount(SelectedClient, TxtTransferToClient, currentUser);
                        }
                    }
                    ResetValues();
                },
                obj =>
                {
                    if (SelectedClient == null) return false;
                    else return SelectedClient.Account > TxtTransferToClient & TxtTransferToClient > -1;
                }));
            }
        }

        #endregion
    }
}
