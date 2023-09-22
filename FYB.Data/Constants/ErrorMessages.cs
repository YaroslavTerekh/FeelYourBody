using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Constants;

public static class ErrorMessages
{
    public const string WrongPassword = "Пароль неправильний";

    public const string FAQNotFound = "FAQ не знайдено";
    public const string CoachNotFound = "Тренера не знайдено";
    public const string CoachingNotFound = "Тренування не знайдено";
    public const string CoachDetailNotFound = "Деталі тренера не знайдено";
    public const string CoachingDetailNotFound = "Деталі тренування не знайдено";
    public const string CoachingDetailParentNotFound = "Деталі тренування не знайдено";
    public const string CoachingVideoNotFound = "Відео не знайдено";
    public const string FeedbackNotFound = "Відгуку не знайдено";
    public const string FileNotFound = "Файлу не знайдено";
    public const string FoodNotFound = "Харчування не знайдено";
    public const string FoodPointNotFound = "Пункту харчування не знайдено";
    public const string UserNotFound = "Користувача не знайдено";
    public const string PurchaseNotFound = "Покупку не знайдено";
    public static string ProductNotFound(string product) => $"{product} не знайдено";

    public const string UnknownError = "На сервері сталась невідома помилка, або ви вказали невірні дані, спробуйте ще раз";
    public const string UnknownVideoType = "Невідомий тип даних (дозволені типи: .mp4)";
    public const string UnknownPhotoType = "Невідомий тип даних (дозволені типи: .jpg; .jpeg; .png; ";

    public const string ProductAlreadyBought = "Ви вже придбали цей продукт";
    public const string UserWithNumberExists = "Користувач з таким номером телефону вже присутній";
    public const string PhoneNumberAlreadyConfirmed = "Ви вже підтвердили свій номер телефону";
    public const string PhoneNumberIsNotConfirmed = "Спочатку підтвердьте свій номер телефону";
    public const string CodeNotValid = "Код не правильний";
    public const string ContentAccessForbidden = "Доступ до контенту заборонено";
    public const string PreviewExists = "Прев'ю тренування вже присутнє";
    public const string WrongPhoneNumber = "Номер телефону некоректний";
}
