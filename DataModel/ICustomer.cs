using Mock2021DataLayer.DataModel;
using System.Collections.Generic;

namespace RadMock2021.DataModel
{
    // make sure it is public
    public interface ICustomer
    {
        // get customer Id
        Customer Get(int id);

        // use ICollection for the getting all the customers in a  list
        ICollection<Customer> GetCustomerList();

        // get topup history using customer id which is forigen key in that table.
        Customer getCustomerTopupHistory(int customerID);

        // get purchase history using customer id which is forigen key in that table.

        Customer getCustomerPurchaseHistory(int customerID);
    }

    // IResource class (make public) this is for our online resouce
    public interface IResource
    {
        // get online resouce details using PK which is Resource ID.
        OnlineResource GetResouceDetails(int id);
        
        // use ICollection for listing the all online resource
        ICollection<OnlineResource> GetResoursesList();

    }
}
