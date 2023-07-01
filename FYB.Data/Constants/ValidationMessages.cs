using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Constants;

public static class ValidationMessages
{
    public const string IdRequired = "Необхідно вказати Id";
    public const string IdWrong = "Вказане Id некоректне";

    public const string FirstNameTooShort = "Ім'я занадто коротке";
    public const string FirstNameTooLong = "Ім'я занадто довге";
    public const string FirstNameRequired = "Необхідно ввести ім'я";

    public const string LastNameTooShort = "Прізвище занадто коротке";
    public const string LastNameTooLong = "Прізвище занадто довге";
    public const string LastNameRequired = "Необхідно ввести прізвище";

    public const string WrongEmail = "Введіть коректний email";
    public const string EmailRequired = "Необхідно ввести email";

    public const string FAQTooShort = "FAQ занадто коротке";
    public const string FAQTooLong = "FAQ занадто довге";
    public const string FAQRequired = "Необхідно ввести FAQ";
    public const string FAQAnswerTooShort = "Відповідь на питання занадто коротка";
    public const string FAQAnswerTooLong = "Відповідь на питання занадто довга";
    public const string FAQAnswerRequired = "Необхідно ввести відповідь на питання";

    public const string TitleRequired = "Необхідно ввести назву";
    public const string TitleTooLong = "Назва занадто довга";
    public const string TitleTooShort = "Назва занадто коротка";

    public const string DescriptionRequired = "Необхідно ввести опис";
    public const string DescriptionTooLong = "Опис занадто довгий";
    public const string DescriptionTooShort = "Опис занадто короткий";

    public const string InstagramLinkRequired = "Необхідно ввести посилання на сторінку Instagram";

    public const string WrongDateTime = "Вказано некоректну дату";
    public const string WrongPrice = "Вказано некоректну ціну";
}
