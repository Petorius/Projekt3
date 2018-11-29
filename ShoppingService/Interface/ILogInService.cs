﻿using System.ServiceModel;

namespace Server.ServiceLayer {
    [ServiceContract]
    public interface ILoginService {
        [OperationContract]
        bool CreateUserWithPassword(string firstName, string lastName, string street,
                        int zip, string city, string email, int number, string password);

        [OperationContract]
        bool ValidatePassword(string email, string password);
        [OperationContract]
        bool ValidateAdminLogin(string email, string password);
    }
}