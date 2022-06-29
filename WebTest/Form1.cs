using System.Security;
using System.Text;

namespace WebTest
{
    public partial class Form1 : Form
    {
        // 0. ������� ������� ��� ������ �� ��������� http
        HttpClient client = new ();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            // ��� ������ ���������� ���������� �������� ���� "pic.jpg"
            // � ����� � ����������� ������.
            // ����� ���������� �������� PHP-������ ��� ��������� �������� ����������

            // �������� POST-������� � �������������� �������
            // ��� ������� ���������� ����� + ����
            
            // 1. ������� ������� ��� ���������� ���� ������ "�����".
            var formContent = new MultipartFormDataContent();

            // 2. ��������� ��������� �����.
            // 2�. ����������� ����������� ������ � ���� ������� (�������� �����, ��������)
            var vals = new Dictionary<string, string>
            {
                { "filename", "pic.jpg" }
            };
            // 2�. �������� ������� ��� �������� �� ������
            var tcontent = new FormUrlEncodedContent(vals);

            // 3. ���������� ������ ����� � ��������
            // 3�. ������ ���� ��� ������ ������
            var file = File.ReadAllBytes("pic.jpg");
            // 3�. ������������� ������ � ���, ��������� ��� �������� �� ������
            var fcontent = new ByteArrayContent(file);
            
            // 4. ��������� ��������� � �������� ������� � ����� ������������� ����������
            formContent.Add(tcontent, "data");
            formContent.Add(fcontent, "file");

            // 5. ��������� ��������������� ��� ������.
            client.PostAsync("http://2k-lect/test.php", formContent);

            // � ������ ������������� �������� ������ ��������� ������,
            // ��. 1, 3, 4 ����� ����������.

            // ������ ������������ �������� ����� ������� test.php ������� POST
            // �� ���������� �������:
            // data="filename=pic.jpg"&file="...���������� ������������ �����..."
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            // �������� �� ������ GET-�������
            client.GetAsync("http://2k-lect/test.php");
        }
    }
}