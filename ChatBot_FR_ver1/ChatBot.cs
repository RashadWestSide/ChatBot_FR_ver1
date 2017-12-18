using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ChatBot_FR_ver1
{
    class ChatBot
    {
        string question;   // вопрос
        string path;    // путь БД
        string userAnswer;  // ответ пользователя (для обучения)

        List<string> BD = new List<string>(); // БД
        bool flag = true;   // переключатель (бот или учится, или отвечает)

        public event Action<string> GetStr; // событие

        // конструктор
        public ChatBot(string _path)
        {
            path = _path;   // запись пути

            // блок пытается отобразить(найти) БД на выбранном пути, если её нет, то ничего не делается
            try
            {
                BD.AddRange(File.ReadAllLines(_path));
            }
            catch
            {

            }

            // событие когда бот отвечает
            GetStr += ChatBot_GetStr;

            GetStr("\nВаш вопрос: ");
        }

        // заглушка (чтобы прога не вылетела, если на событие нет подписки)
        void ChatBot_GetStr(string obj)
        {

        }


        // Обучение
        private void Teach()
        {
            BD.Add(question);  // добавление вопроса
            BD.Add(userAnswer); // добвление ответ
            File.WriteAllLines(path, BD.ToArray());    //сохранение
        }


        //Удаление символов
        static string DelSym(string str, char[] symbols)
        {
            string strA = str;  // копирование строки

            //Удаление символов
            for (int i = 0; i < symbols.Length; i++)
            {
                strA = strA.Replace(char.ToString(symbols[i]), "");
            }

            return strA;
        }

        // поиск ответа
        private string SrcAnsw(string qw)
        {
            string delSymb = ")(:^^=!?";    //символы которые удаляем
            string ans = string.Empty;    // ответ бота

            qw = qw.ToLower();    // перевод в нижн. регистр
            qw = DelSym(qw, delSymb.ToCharArray());   // удаление букв

            // Поиск по БД
            for (int i = 0; i < BD.Count; i += 2)
            {
                if (qw == BD[i])
                {
                    ans = BD[i + 1];    // выдает ответ
                    break;  //завершаем цикл
                }
            }

            return ans; //Ответ
        }


        // генерация ответа
        public void Answ(string _question)
        {
            if (flag)
            {
                question = _question;
                string ans = SrcAnsw(_question);

                // переход в режим обучения
                if (ans == string.Empty)
                {
                    flag = false;
                    GetStr("Введите ответ: ");
                }
                // режим ответа
                else GetStr("Ответ бота: " + ans + "\n\nВаш вопрос: ");   // ответ
            }

            else
            {
                flag = true;
                userAnswer = _question;
                Teach();
                GetStr("\nВаш вопрос: ");
            }
        }
    }
}
