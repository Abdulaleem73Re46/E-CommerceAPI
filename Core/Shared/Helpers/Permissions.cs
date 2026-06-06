
namespace Core.Shared.Helpers;

public static class Permissions
{
    // أذونات المنتجات (Product)
    public const string Product_View = "Product.View";
    public const string Product_Create = "Product.Create";
    public const string Product_Edit = "Product.Edit";
    public const string Product_Delete = "Product.Delete";

    // أذونات الطلبات (Order)
    public const string Order_View = "Order.View";
    public const string Order_Create = "Order.Create";
    public const string Order_Edit = "Order.Edit";
    public const string Order_Delete = "Order.Delete";
    public const string Order_Approve = "Order.Approve";

    // أذونات المستخدمين (User)
    public const string User_View = "User.View";
    public const string User_Create = "User.Create";
    public const string User_Edit = "User.Edit";
    public const string User_Delete = "User.Delete";

    // يمكنك إضافة المزيد حسب تطبيقك ...
}