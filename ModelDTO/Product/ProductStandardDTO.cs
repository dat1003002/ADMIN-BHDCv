namespace AspnetCoreMvcFull.ModelDTO.Product
{
  public class ProductStandardDTO
  {
    public int ProductId { get; set; }
    public string? name { get; set; } // Tên hướng dẫn (ví dụ: "Hướng dẫn kiểm tra sản phẩm A")
    public string? image { get; set; } // Tên file hình ảnh
    public IFormFile? imageFile { get; set; } // File upload
    public int CategoryId { get; set; } // Fixed 15
  }
}
