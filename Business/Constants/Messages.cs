using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
   public static class Messages
    {

        public static string BrandAdded = "Marka eklendi";
        public static string BrandList = "Marka Listelendi";
        public static string ColorAdded = "Renk Eklendi";
        public static string ColorDeleted = "Renk silindi";
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CarAdded = "Araba eklendi";
        public static string CarDeleted = "Araba silindi";
        public static string RentalValidator = "CustomerId boş geçilemez";
        public static string CheckIfBrandIdCount = "Bir markadan en fazla 9 araç olabilir";
        public static string CheckIfColorCount = "Bir renkten en fazla 9 araç olabilir";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UsersAdded = "Kullanıcı oluşturuldu";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Başka bir kullanıcı ismi giriniz";
        public static string AccessTokenAdded = "Token oluşturuldu";
        public static string TransactionsCars = "Transaction başarılı";
        public static string CarUpdated = "Araç güncellendi";
    }
}
