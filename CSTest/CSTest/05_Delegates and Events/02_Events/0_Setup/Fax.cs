using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events._02_Events._0_Setup
{
    internal sealed class Fax
    {
        // Передаем конструктору объект MailManager
        public Fax(MailManager mm)
        {
            // Создаем экземпляр делегата EventHandler<NewMailEventArgs>,
            // ссылающийся на метод обратного вызова FaxMsg
            // Регистрируем обратный вызов для события NewMail объекта MailManager
            mm.NewMail += FaxMsg;
            //mm.add_NewMail(new EventHandler<NewMailEventArgs>(this.FaxMsg));
        }

        // MailManager вызывает этот метод для уведомления
        // объекта Fax о прибытии нового почтового сообщения
        private void FaxMsg(object sender, NewMailEventArgs e)
        {
            // 'sender' используется для взаимодействия с объектом MailManager,
            // если потребуется передать ему какую-то информацию
            // 'e' определяет дополнительную информацию о событии,
            // которую пожелает предоставить MailManager
            // Обычно расположенный здесь код отправляет сообщение по факсу
            // Тестовая реализация выводит информацию на консоль
            Debug.WriteLine("Faxing mail message:");
            Debug.WriteLine(" From={0}, To={1}, Subject={2}",
                e.From, e.To, e.Subject);
        }

        // Этот метод может выполняться для отмены регистрации объекта Fax
        // в качестве получтеля уведомлений о событии NewMail
        public void Unregister(MailManager mm)
        {
            // Отменить регистрацию на уведомление о событии NewMail объекта
            mm.NewMail -= FaxMsg;
            //mm.remove_NewMail(new EventHandler<NewMailEventArgs>(FaxMsg));
        }
    }
}