using Mock2021DataLayer.DataModel;
using System.Collections.Generic;
using System.Linq;
using System;


namespace RadMock2021.DataModel
{
    /* Remember to impalment which can be done by
    (right click when underlined in red to implement
     after writing the class name and the I... parts)
     */

    public class CustomerResourceRepo : IResource, ICustomer
    {

        // create the subscription Db context
        // which is already set up make sure that the subscription is public

        Subscription_DB_Context subscriptionDB;

        public CustomerResourceRepo(Subscription_DB_Context subscriptionDB)
        {
 
            this.subscriptionDB = subscriptionDB;
        }

        public Customer Get(int id)
        {  // use the subscription to access the customer and add a return
            // find custoemer by ID
            return subscriptionDB.Customers.Find(id);

        }

        public ICollection<Customer> GetCustomerList()
        {
            // get will get all customers
            return subscriptionDB.Customers.ToList();
        }

        public Customer getCustomerPurchaseHistory(int customerID)
        {
            // create a customer
            Customer result = new Customer();
            // select the customer via id
            var SelectedCustomer = Get(customerID);
            result = SelectedCustomer;
            // add customers purchase history to a list.
            result.PurchaseHisories = SelectedCustomer.PurchaseHisories.ToList();
            return result;
            //throw new System.NotImplementedException();
        }

        public Customer getCustomerTopupHistory(int customerID)
        {
            // create a customer
            Customer result = new Customer();
            // select the customer via id
            var SelectedCustomer = Get(customerID);
            result = SelectedCustomer;
            // add customers TopUp History to a list.
            result.TopUpHistories = SelectedCustomer.TopUpHistories.ToList();
            // return the customer
            return result;
            //throw new system.notimplementedexception();
        }

        public OnlineResource GetResouceDetails(int id)
        {
            // finds the Online Resouce by customers id
            return subscriptionDB.OnlineResources.Find(id);
            //throw new System.NotImplementedException();
        }

        public ICollection<OnlineResource> GetResoursesList()
        {
            // get will get all the Online resources
            return subscriptionDB.OnlineResources.ToList();

            //throw new System.NotImplementedException();
        }
    }
}
