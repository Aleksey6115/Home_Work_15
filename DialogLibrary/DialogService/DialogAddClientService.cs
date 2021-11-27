using System;
using System.Collections.ObjectModel;
using ClientLibrary.Model;
using DialogLibrary.DialogWindows;

namespace DialogLibrary
{
    /// <summary>
    /// ������ ���� - ���������� ������ �������
    /// </summary>
    public class DialogAddClientService
    {
        /// <summary>
        /// ��������� ������
        /// </summary>
        public ClientAbstract client { get; set; }

        /// <summary>
        /// ������ ��������� �������� �������� ��� ��������
        /// </summary>
        private ObservableCollection<ClientAbstract> clientsList = new ObservableCollection<ClientAbstract>
        {
            new ClientIndividual(),
            new ClientBusiness(),
            new ClientVIP()
        };
        private ObservableCollection<int> ageList = GeneratorAge();

        /// <summary>
        /// �������� ����������� ���� "�������� �������"
        /// </summary>
        /// <returns></returns>
        public bool OpenAddClientDialog()
        {
            AddClientWindow ACW = new AddClientWindow();
            ACW.comboStatusClient.Items.Clear();
            ACW.comboAgeClient.Items.Clear();
            ACW.comboStatusClient.ItemsSource = clientsList;
            ACW.comboAgeClient.ItemsSource = ageList;

            if (ACW.ShowDialog() == true)
            {
                if (ACW.comboStatusClient.SelectedItem != null && ACW.comboAgeClient.SelectedItem != null &&
                    ACW.txtNameClient.Text != null && ACW.txtLastNameClient.Text != null)
                {
                    client = ACW.comboStatusClient.SelectedItem as ClientAbstract;
                    client.FirstName = ACW.txtNameClient.Text;
                    client.LastName = ACW.txtLastNameClient.Text;
                    client.Age = (int)ACW.comboAgeClient.SelectedItem;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ������� ������ ��� ������ ��������
        /// </summary>
        /// <returns></returns>
        static ObservableCollection<int> GeneratorAge()
        {
            ObservableCollection<int> result = new ObservableCollection<int>();

            for (int i = 18; i < 64; i++)
                result.Add(i);

            return result;
        }
    }
}
