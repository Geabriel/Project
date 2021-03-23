using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Parser
{
    public class ResultList
    {
        public ResultList(
            string url, string header, string discriplion, string city, string supplier,
            string[] phone, uint id = 0, string category=null, string email = null,
            string dateUblication = null, string dateUpdate = null, string[] priceProduct = null)
        {
            Url = url;
            Header = header;
            Discriplion = discriplion;
            City = city;
            Supplier = supplier;
            Phone = phone;
            Id = id;
            Category = category;
            Email = email;
            DateUblication = dateUblication;
            DateUpdate = dateUpdate;
            PriceProduct = priceProduct;
        }
        #region Required fields. Обязательные поля
        public string Url { get; private set; } //сылка на обяву
        public string Header { get; private set; } //заголовог объявы
        public string Discriplion { get; private set; } //Описание
        public string City { get; private set; } //Город
        public string Supplier { get; private set; } // Имя или название предприятия
        public string[] Phone { get; private set; } // телефон в случае если он не один
        #endregion

        #region Optional fields. Не обязательные поля
        public uint Id { get; private set; } // Id объявлния
        public string Category { get; private set; } //категория
        public string Email { get; private set; } //адрес почты
        public string DateUblication { get; private set; } //дата публикации
        public string DateUpdate { get; private set; } // дата обновления поста
        public string[] PriceProduct { get; private set; } // стоимость товара
        #endregion
    }
}
