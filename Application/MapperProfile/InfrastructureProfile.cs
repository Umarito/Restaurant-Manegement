using AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
         CreateMap<User,UserGetDto>();
         CreateMap<User,UserGetAsWaiterDto>();
        CreateMap<UserInsertDto,User>();
        CreateMap<UserUpdateDto,User>();
        CreateMap<Table,TableGetDto>();
        CreateMap<TableInsertDto,Table>();
        CreateMap<TableUpdateDto,Table>();
        CreateMap<Payment,PaymentGetDto>();
        CreateMap<Payment,PaymentGetWithOrderDto>();
        CreateMap<PaymentInsertDto,Payment>();
        CreateMap<PaymentUpdateDto,Payment>();
        CreateMap<Order,OrderGetDto>();
        CreateMap<Order,OrderGetWithTableAndWaiterJoinDto>();
        CreateMap<OrderInsertDto,Order>();
        CreateMap<OrderUpdateDto,Order>();
        CreateMap<OrderItem,OrderItemGetDto>();
        CreateMap<OrderItem,OrderItemGetWithOrderDto>();
        CreateMap<OrderItemInsertDto,OrderItem>();
        CreateMap<OrderItemUpdateDto,OrderItem>();
        CreateMap<MenuItem,MenuItemGetDto>();
        CreateMap<MenuItem,MenuItemGetWithCategoryDto>();
        CreateMap<MenuItemInsertDto,MenuItem>();
        CreateMap<MenuItemUpdateDto,MenuItem>();
        CreateMap<Category,CategoryGetDto>();
        CreateMap<Category,CategoryGetWithMenuItemsDto>();
        CreateMap<CategoryInsertDto,Category>();
        CreateMap<CategoryUpdateDto,Category>();
    }
}