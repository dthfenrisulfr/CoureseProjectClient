using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using CPS;

namespace ElinCoureseProjectClient.Data
{
    public static class GrpcClient
    {
        public static bool Connection()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5000");
            _client = new ELIN_CPS.ELIN_CPSClient(channel);
            _cts = new System.Threading.CancellationToken();
            return true;
        }


        public static Positions GetOnePosition(int IDrequest)
        {
            var request = new PositionRequest();
            request.PositionID = IDrequest;
            var response = _client.GetOneFromPosition(request);
            return response;
        }
        public async static Task<List<Positions>> GetPositionList()
        {
            var request = new PositionRequest();
            request.PositionID = 1;
            var positions = new List<Positions>();
            using (var call = _client.GetPositionList(request))
            {
                while (await call.ResponseStream.MoveNext(_cts))
                {
                    positions.Add(call.ResponseStream.Current);
                }
            }
            return positions;
        }
        public static void AddPosition(Models.ForTwoColumnModalModel positionModel)
        {
            var position = new Positions();
            position.Position = positionModel.FirstColumn;
            position.Salary = Convert.ToDouble(positionModel.SecondColumn);
            _client.AddPosition(position);
        }
        public static void AddDepatment(Models.ForTwoColumnModalModel positionModel)
        {
            var departments = new Departments();
            departments.Department = positionModel.FirstColumn;
            departments.NumberOfEmployees = Convert.ToInt32(positionModel.SecondColumn);
            _client.AddDepartment(departments);
        }
        public static void AddStaff(Staff staff)
        {
            _client.AddStaff(staff);
        }
        public static void DeletePosition(int IDrequest)
        {

        }
        public static void UpdatePosition(int IDrequest, Positions newPosition)
        {

        }


        public async static Task<List<Orders>> GetOrdersList()
        {
            var request = new OrderRequest();
            request.OrderID = 1;
            var orders = new List<Orders>();
            using (var call = _client.GetOrderList(request))
            {
                while (await call.ResponseStream.MoveNext(_cts))
                {
                    orders.Add(call.ResponseStream.Current);
                }
            }
            return orders;
        }
        public async static Task<List<Customers>> GetCustomersList()
        {
            var request = new CustomerRequest();
            request.CustomerID = 1;
            var customers = new List<Customers>();
            using (var call = _client.GetCustomersList(request))
            {
                while (await call.ResponseStream.MoveNext(_cts))
                {
                    customers.Add(call.ResponseStream.Current);
                }
            }
            return customers;
        }
        public async static Task<List<Staff>> GetStaffList()
        {
            var request = new StaffRequest();
            request.Passport = 1;
            var staff = new List<Staff>();
            using (var call = _client.GetStaffList(request))
            {
                while (await call.ResponseStream.MoveNext(_cts))
                {
                    staff.Add(call.ResponseStream.Current);
                }
            }
            return staff;
        }
        public async static Task<List<Products>> GetProductsList()
        {
            var request = new ProductRequest();
            request.ProductID = 1;
            var products = new List<Products>();
            using (var call = _client.GetProductList(request))
            {
                while (await call.ResponseStream.MoveNext(_cts))
                {
                    products.Add(call.ResponseStream.Current);
                }
            }
            return products;
        }
        public async static Task<List<Departments>> GetDepatmentsList()
        {
            var request = new DepartmentRequest();
            request.DepartmentID = 1;
            var departments = new List<Departments>();
            using (var call = _client.GetDepartmentList(request))
            {
                while (await call.ResponseStream.MoveNext(_cts))
                {
                    departments.Add(call.ResponseStream.Current);
                }
            }
            return departments;
        }


        static ELIN_CPS.ELIN_CPSClient _client;
        static System.Threading.CancellationToken _cts;
    }
}
