using System.Security;
using System.Text;

namespace WebTest
{
    public partial class Form1 : Form
    {
        // 0. Создаем клиента для работы по протоколу http
        HttpClient client = new ();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            // Для работы приложения необходимо положить файл "pic.jpg"
            // В папку с исполняемым файлом.
            // Также необходимо написать PHP-скрипт для обработки запросов приложения

            // Отправка POST-запроса с мультичастными данными
            // Для примера отправляем текст + файл
            
            // 1. Создаем контент для содержания всех данных "формы".
            var formContent = new MultipartFormDataContent();

            // 2. Формируем текстовую часть.
            // 2а. Располагаем необходимые данные в виде словаря (название ключа, значение)
            var vals = new Dictionary<string, string>
            {
                { "filename", "pic.jpg" }
            };
            // 2б. Кодируем словарь для передачи на сервер
            var tcontent = new FormUrlEncodedContent(vals);

            // 3. Организуем данные файла к отрпавке
            // 3а. Читаем файл как массив байтов
            var file = File.ReadAllBytes("pic.jpg");
            // 3б. Конверртируем массив в тип, пригодный для отправки на сервер
            var fcontent = new ByteArrayContent(file);
            
            // 4. Добавляем текстовый и файловый контент в общее мультичастное содержимое
            formContent.Add(tcontent, "data");
            formContent.Add(fcontent, "file");

            // 5. Формируем непосредственно сам запрос.
            client.PostAsync("http://2k-lect/test.php", formContent);

            // В случае необходимости передачи только текстовых данных,
            // пп. 1, 3, 4 можно пропустить.

            // Запрос эквивалентен отправке формы скрипту test.php методом POST
            // со следующими данными:
            // data="filename=pic.jpg"&file="...содержимое прочитанного файла..."
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            // Отправка на сервер GET-запроса
            client.GetAsync("http://2k-lect/test.php");
        }
    }
}