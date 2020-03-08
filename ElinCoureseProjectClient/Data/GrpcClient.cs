using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using CPS;

namespace ElinCoureseProjectClient.Data
{
    /// <summary>
    ///  Класс создающий соединение с сервисом
    /// </summary>
    public static class GrpcClient
    {
        /// <summary>
        ///  Статический конструктор класса
        /// </summary>
        static GrpcClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5000");
            _client = new ELIN_CPS.ELIN_CPSClient(channel);
            _cts = new System.Threading.CancellationToken();
        }

        /// <summary>
        ///  Метод через энумератор выбирает один из методов, делает запрос на свервис и возвращает выбранный объект из коллекции БД
        /// </summary>
        public static object GetOneObject(int objectID, ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Position:
                    return _client.GetOneFromPosition(new PositionRequest { PositionID = objectID });

                case ObjectType.Depatment:
                    return _client.GetOneFromDepartment(new DepartmentRequest { DepartmentID = objectID });

                case ObjectType.Staff:
                    return _client.GetOneFromStaff(new StaffRequest { Passport = objectID });

                case ObjectType.Order:
                    return _client.GetOneFromOrders(new OrderRequest { OrderID = objectID });

                case ObjectType.Product:
                    return _client.GetOneFromProducts(new ProductRequest { ProductID = objectID });

                case ObjectType.Customer:
                    return _client.GetOneFormCustomers(new CustomerRequest { CustomerID = objectID });
            }
            return null;
        }
        /// <summary>
        ///  Метод через энумератор выбирает один из методов, делает запрос на свервис и возвращает коллекцию выбранного типа
        /// </summary>
        public async static Task<object> GetObjectList(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Position:
                    var positions = new List<Positions>();
                    using (var call = _client.GetPositionList(new PositionRequest { PositionID = 1 }))
                    {
                        while (await call.ResponseStream.MoveNext(_cts))
                        {
                            positions.Add(call.ResponseStream.Current);
                        }
                    }
                    return positions;

                case ObjectType.Depatment:
                    var departments = new List<Departments>();
                    using (var call = _client.GetDepartmentList(new DepartmentRequest { DepartmentID = 1 }))
                    {
                        while (await call.ResponseStream.MoveNext(_cts))
                        {
                            departments.Add(call.ResponseStream.Current);
                        }
                    }
                    return departments;

                case ObjectType.Staff:
                    var staff = new List<Staff>();
                    using (var call = _client.GetStaffList(new StaffRequest { Passport = 1 }))
                    {
                        while (await call.ResponseStream.MoveNext(_cts))
                        {
                            staff.Add(call.ResponseStream.Current);
                        }
                    }
                    return staff;

                case ObjectType.Order:
                    var orders = new List<Orders>();
                    using (var call = _client.GetOrderList(new OrderRequest { OrderID = 1 }))
                    {
                        while (await call.ResponseStream.MoveNext(_cts))
                        {
                            orders.Add(call.ResponseStream.Current);
                        }
                    }
                    return orders;

                case ObjectType.Product:
                    var products = new List<Products>();
                    using (var call = _client.GetProductList(new ProductRequest { ProductID = 1 }))
                    {
                        while (await call.ResponseStream.MoveNext(_cts))
                        {
                            products.Add(call.ResponseStream.Current);
                        }
                    }
                    return products;

                case ObjectType.Customer:
                    var customers = new List<Customers>();
                    using (var call = _client.GetCustomersList(new CustomerRequest { CustomerID = 1 }))
                    {
                        while (await call.ResponseStream.MoveNext(_cts))
                        {
                            customers.Add(call.ResponseStream.Current);
                        }
                    }
                    return customers;
            }
            return null;
        }
        /// <summary>
        ///  Метод через энумератор выбирает один из методов, делает запрос на добавление
        /// </summary>
        public static void AddObject(object Obj, ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Position:
                    var position = new Positions();
                    position.Position = (Obj as Models.ForTwoColumnModalModel).FirstColumn;
                    position.Salary = Convert.ToDouble((Obj as Models.ForTwoColumnModalModel).SecondColumn);
                    _client.AddPosition(position);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Staff:
                    _client.AddStaff(Obj as Staff);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Depatment:
                    var departments = new Departments();
                    departments.Department = (Obj as Models.ForTwoColumnModalModel).FirstColumn;
                    departments.NumberOfEmployees = Convert.ToInt32((Obj as Models.ForTwoColumnModalModel).SecondColumn);
                    _client.AddDepartment(departments);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Customer:
                    _client.AddSCustomer(Obj as Customers);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Order:
                    _client.AddOrder(Obj as Orders);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Product:
                    _client.AddProduct(Obj as Products);
                    StateChanged?.Invoke("base", null);
                    break;
            }
        }
        /// <summary>
        ///  Метод через энумератор выбирает один из методов, делает запрос на удаление
        /// </summary>
        public static void DeleteObject(int objcetID, ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Position:
                    _client.DeletePosition(new PositionRequest { PositionID = objcetID });
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Product:
                    _client.DeleteProduct(new ProductRequest { ProductID = objcetID });
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Staff:
                    _client.DeleteStaff(new StaffRequest { Passport = objcetID });
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Depatment:
                    _client.DeleteDepartment(new DepartmentRequest { DepartmentID = objcetID });
                    StateChanged?.Invoke("base", null);
                    break;
            }
        }
        /// <summary>
        ///  Метод через энумератор выбирает один из методов, делает запрос на обновление
        /// </summary>
        public static void UpdateObject(object Obj, ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Position:
                    _client.UpdatePosition(Obj as Positions);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Staff:
                    _client.UpdateStaff(Obj as Staff);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Depatment:
                    _client.UpdateDepartment(Obj as Departments);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Product:
                    _client.UpdateProduct(Obj as Products);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Order:
                    _client.UpdateOrder(Obj as Orders);
                    StateChanged?.Invoke("base", null);
                    break;

                case ObjectType.Customer:
                    _client.UpdateCustomer(Obj as Customers);
                    StateChanged?.Invoke("base", null);
                    break;
            }

        }

        /// <summary>
        ///  Событие уведомляющее об изменении состояния коллекции
        /// </summary>
        public static EventHandler StateChanged { get; set; }

        static ELIN_CPS.ELIN_CPSClient _client;
        static System.Threading.CancellationToken _cts;
    }
}
