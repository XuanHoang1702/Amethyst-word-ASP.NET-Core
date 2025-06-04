using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Share.Consts
{
    public class StoreProcedureConsts
    {
        //User
        public const string USER_Lst = "USER_Lst";
        public const string USER_Register = "USER_Register";
        public const string USER_Login = "USER_Login";
        public const string USER_Up = "USER_Up";
        public const string USER_Inf = "USER_Inf";
        public const string USER_UPSERT_Google = "USER_UPSERT_Google";

        //Address
        public const string ADDRESS_Get = "ADDRESS_Get";
        public const string ADDRESS_Create = "ADDRESS_Create";
        public const string ADDRESS_Up = "ADDRESS_Up";

        //Admin
        public const string ADMIN_Login = "ADMIN_Login";
        public const string ADMIN_Create_Acount = "ADMIN_Create_Acount";
        public const string ADMIN_Des = "ADMIN_Des";

        //Brand
        public const string BRAND_List = "BRAND_List";
        public const string BRAND_Create = "BRAND_Create";
        public const string BRAND_Update = "BRAND_Update";
        public const string Brand_Delete = "Brand_Delete";

        //Category
        public const string CATEGORY_List = "CATEGORY_List";
        public const string CATEGORY_Create = "CATEGORY_Create";
        public const string CATEGORY_Update = "CATEGORY_Update";
        public const string CATEGORY_Delete = "CATEGORY_Delete";

        //Product
        public const string PRODUCT_List = "PRODUCT_List";
        public const string PRODUCT_Detail = "PRODUCT_Detail";
        public const string PRODUCT_Search = "PRODUCT_Search";
        public const string PRODUCT_Fillter = "PRODUCT_Fillter";
        public const string PRODUCT_Create = "PRODUCT_Create";
        public const string PRODUCT_Delete = "PRODUCT_Delete";
        public const string PRODUCT_Update = "PRODUCT_Update";
        public const string PRODUCT_Related = "PRODUCT_Related";
        public const string PRODUCT_New = "PRODUCT_New";
        public const string PRODUCT_BestSeller = "PRODUCT_BestSeller";
        public const string PRODUCT_IMAGE_Create = "PRODUCT_IMAGE_Create";
        public const string COLOR_Product = "COLOR_Product";
        public const string SIZE_Product = "SIZE_Product";


        //Discount
        public const string DISCOUNT_List = "DISCOUNT_List";
        public const string DISCOUNT_Create = "DISCOUNT_Create";
        public const string DISCOUNT_Update = "DISCOUNT_Update";
        public const string DISCOUNT_Delete = "DISCOUNT_Delete";
        public const string DISCOUNT_NoPaging = "DISCOUNT_NoPaging";

        //WishList
        public const string WISHLIST_List = "WISHLIST_List";
        public const string WISHLIST_Create = "WISHLIST_Create";
        public const string WISHLIST_Delete = "WISHLIST_Delete";

        //Contact
        public const string CONTACT_List = "CONTACT_List";
        public const string CONTACT_Update = "CONTACT_Update";
        public const string CONTACT_Create = "CONTACT_Create";

        //Cart
        public const string CART_Create = "CART_Create";
        public const string CART_Delete = "CART_Delete";
        public const string CART_List = "CART_List";


        //Menu
        public const string MENU_List = "MENU_List";
        public const string MENU_Create = "MENU_Create";
        public const string MENU_Update = "MENU_Update";
        public const string MENU_Delete = "MENU_Delete";

        //Stock
        public const string STOCK_List = "STOCK_List";

        //Auth
        public const string REFRESH_TOKEN_Create = "REFRESH_TOKEN_Create";
        public const string REFRESH_TOKEN_ById = "REFRESH_TOKEN_ById";
        public const string REFRESH_TOKEN_Update = "REFRESH_TOKEN_Update";
        public const string DEVICES_Check = "DEVICES_Check";
        public const string OTP_STORE_Check = "OTP_STORE_Check";


        //Order
        public const string ORDER_Create = "ORDER_Create";
        public const string ORDER_DETAIL_Create = "ORDER_DETAIL_Create";
        public const string ORDER_Update = "ORDER_Update";
        public const string ORDER_STATUS = "ORDER_STA";
        public const string ORDER_New = "ORDER_New";
        public const string ORDER_List = "ORDER_List";

        //OTP 
        public const string OTP_STORE_Create = "OTP_STORE_Create";

        //Dash board
        public const string USER_Quantity = "USER_Quantity";
        public const string ORDER_Quantity = "ORDER_Quantity";
        public const string ORDER_Revenue = "ORDER_Revenue";
        public const string REVENUE_Month = "REVENUE_Month";
        public const string REVENUE_Week = "REVENUE_Week";
        public const string REVENUE_Total = "REVENUE_Total";

        //Rat Comment
        public const string RATE_COMMENT_Create = "RATE_COMMENT_Create";
        public const string RATE_COMMENT_List = "RATE_COMMENT_List";
    }
} 
