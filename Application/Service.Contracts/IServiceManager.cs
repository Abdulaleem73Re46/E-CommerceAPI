

namespace Service.Contracts;



public interface IServiceManager
{
    
 ICartService CartService{get;}
 ICategoryService CategoryService{get;}
 IProductService ProductService{get;}
 IPaymentService PaymentService{get;}
 IOrderService OrderService{get;}   




}



/*





namespace Core.Shared.Catalog;


public record CategoryDto
{
    public Guid CategoryId{get;init;}

    public string Name{get;init;}
    public DateTime CreatedAt{get;set;}

   //public ICollection<Product> 



}


public record CreateCategoryDto
{
    

    public string Name{get;init;}=string.Empty;
    

   //public ICollection<Product> 



}
public record UpdateCategoryDto
{
    

    public string Name{get;init;}
  

   //public ICollection<Product> 



}



using System.Diagnostics.Contracts;

namespace Core.Shared.OrderDto;




public record OrderDto
{
    

    public Guid OrderId{get;init;}
    public Guid UserId{get;init;}
     public decimal TotalPrice{get;init;}
  public DateTime OrderDate {get;init;}
//public 
   //public Guid {get;init;}
    
    }


public record OrderItem
{
    
    public Guid OrderItemId{get;set;}
   public Guid OrderId{get;set;}


   public Guid ProductId{get;set;}
   public int Quantity{get;set;}
//[Required,Column(TypeName ="decimal(18,2)")]
   public decimal PriceAtPurchase{get;set;}


//[ForeignKey("ProductId")]
   //public ICollection<Product> Products{get;set;}

  
   public Order Order{get;set;}



}    





namespace Core.Shared.ProductDto;




public record ProductDto
{
    public Guid ProductId{get;init;}

    public string Name{get;init;}
    public string Description{get;init;}
     public decimal Price{get;init;}
  public int StockQuantity{get;init;}

   public Guid? CategoryId{get;init;}
   public Category? Category{get;init;}
  
  //public ICollection<CartItem> CartItems{get;init;}
 
    //public DateTime CreatedAt{get;init;}

   //public ICollection<Product> 



}


public record CreateProductDto
{
    

    public string Name{get;init;}
    public string Description{get;init;}
     public decimal Price{get;init;}
  public int StockQuantity{get;init;}

   public Guid? CategoryId{get;init;}
    
    
    
    
    }



    public record UpdateProductDto
{
    

    public string Name{get;init;}
    public string Description{get;init;}
     public decimal Price{get;init;}
  public int StockQuantity{get;init;}

   public Guid? CategoryId{get;init;}
    
    
    
    
    }








    








*/