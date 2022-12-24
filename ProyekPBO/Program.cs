using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class Program {
        static void Main(string[] args) {
            int state = 0;

            AccountManager manager = new AccountManager();
            manager.LoadDatabase();

            Account account = null;

            bool loop = true;
            while (loop) {
                MenuCommand:
                Console.Clear();

                switch (state) {
                    case 0: {
                        if (account == null) {
                            Console.WriteLine("Welcome to \"THIS\" Program");
                            Console.WriteLine("1. Login");
                            Console.WriteLine("2. Register");
                            Console.WriteLine("3. Exit");
                            Console.WriteLine();
                            Console.Write("Choose: ");
                            string input = Console.ReadLine();
                            if (!int.TryParse(input, out var result)) {
                                Console.Clear();
                                Console.WriteLine("Invalid input, press 'any' key to try again");
                                Console.ReadKey();
                                goto MenuCommand;
                            }

                            state = result;
                        } else {
                            Console.WriteLine($"Welcome back, {account.Username}");
                            Console.WriteLine("1. Check Subscription");
                            Console.WriteLine("2. Change/Renew Subscription");
                            Console.WriteLine("3. Logout");
                            Console.WriteLine("4. Exit");
                            Console.WriteLine();
                            Console.Write("Choose: ");
                            string input = Console.ReadLine();
                            if (!int.TryParse(input, out var result)) {
                                Console.Clear();
                                Console.WriteLine("Invalid input, try again");
                                Console.ReadKey();
                                goto MenuCommand;
                            }

                            state = result + 3;
                        }
                         
                        break;
                    }

                    case 1: {
                        Console.WriteLine("Login -- Page (press q to menu)");
                        Console.Write("Username: ");
                        string username = Console.ReadLine();

                        if (username == "q") {
                            state = 0;
                            goto MenuCommand;
                        }

                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        if (username == "" || password == "") {
                            Console.Clear();
                            Console.WriteLine("Invalid input, try again");
                            Console.ReadKey();
                            goto MenuCommand;
                        }

                        bool found = false;
                        Account acc = manager.GetAccount(username);
                        if (acc != null) {
                            if (acc.Password == password) {
                                account = acc;
                                found = true;
                            }
                        }

                        if (!found) {
                            Console.Clear();
                            Console.WriteLine("Account not found, try again");
                            Console.ReadKey();
                            goto MenuCommand;
                        }

                        if (account.CheckSubscription()) {
                            Console.Clear();
                            Console.WriteLine("Your subscription has expired, please renew your subscription");
                            Console.ReadKey();
                        }

                        state = 0;

                        break;
                    }

                    case 2: {
                    REGISTER:
                        Console.Clear();
                        Console.WriteLine("Register -- Page (press q to menu)");
                        Console.Write("Username: ");
                        string username = Console.ReadLine();

                        if (username == "q") {
                            state = 0;
                            goto MenuCommand;
                        }

                        Account acc = manager.GetAccount(username);
                        if (acc != null) {
                            Console.Clear();
                            Console.WriteLine("Account already exists, try again");
                            Console.ReadKey();
                            goto REGISTER;
                        }

                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Address: ");
                        string address = Console.ReadLine();

                        if (username == "" || password == "" || email == "" || address == "") {
                            Console.Clear();
                            Console.WriteLine("Invalid input, try again");
                            Console.ReadKey();
                            goto REGISTER;
                        }

                    SUBCRIPTION:
                        Console.Clear();
                        Console.WriteLine("Select your membership type -- Page");
                        Console.WriteLine("1. Basic Membership (Free)");
                        Console.WriteLine("2. Pro Membership ($5 per month)");
                        Console.WriteLine("3. Enterprise Membership ($7.5 per month/seat)");
                        Console.WriteLine();
                        Console.Write("Choose: ");
                        string input = Console.ReadLine();

                        if (!int.TryParse(input, out var result)) {
                            Console.Clear();
                            Console.WriteLine("Invalid input, try again");
                            Console.ReadKey();
                            goto SUBCRIPTION;
                        }

                        Features feature = Features.None;
                        switch (result) {
                            case 1:
                                feature = Features.BasicMembership;
                                break;
                            case 2:
                                feature = Features.ProMembership;
                                break;
                            case 3:
                                feature = Features.EnterpriseMembership;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid input, try again");
                                Console.ReadKey();
                                goto SUBCRIPTION;
                        }

                        int uid = manager.accounts.Count + 1;

                        Account newAccount;
                        if (feature == Features.EnterpriseMembership) {
                            newAccount = new EnterpriseAccount(uid, username, password, email, address);
                        } else {
                            newAccount = new Account(uid++, username, password, email, address);
                        }

                        newAccount.SetMembership(SubscriptionManager.GetMembership(feature), 10);

                        manager.AddAccount(newAccount);
                        account = newAccount;

                        // Save the database, incase the program crashes
                        manager.SaveDatabase();

                        Console.Clear();
                        Console.WriteLine("Account created, press 'any' key to continue");
                        Console.ReadKey();

                        state = 0;
                        break;
                    }

                    case 3: {
                        Console.WriteLine("Exit, Program now exiting");
                        Console.WriteLine("Press any key to continue...");
                        loop = false;
                        Console.ReadKey();
                        break;
                    }

                    case 4: {
                        Console.WriteLine("Check Subscription -- Page");
                        // Get current clock
                        long time = new DateTime().Ticks;

                        Membership membership = SubscriptionManager.GetMembership(account.Features);
                        if (membership.is_user_own(account)) {
                            Console.WriteLine($"You are currently using {membership.PlanName} membership");
                            DateTime now = DateTime.Now;
                            DateTime expireAt = new DateTime(account.MembershipEndAt);
                            
                            TimeSpan span = expireAt - now;
                            int days = span.Days;

                            Console.WriteLine($"Your membership will expire in {days} days");
                        } else {
                            Console.WriteLine("You are currently not using any membership");
                        }

                        Console.WriteLine("Press any key to back to menu");
                        Console.ReadKey();
                        state = 0;

                        break;
                    }

                    case 5: {
                    SUBCRIPTION:
                        Console.Clear();
                        Console.WriteLine("Select your membership type -- Page");
                        Console.WriteLine("1. Basic Membership (Free)");
                        Console.WriteLine("2. Pro Membership ($5 per month)");
                        Console.WriteLine("3. Enterprise Membership ($7.5 per month/seat)");
                        Console.WriteLine();
                        Console.Write("Choose: ");
                        string input = Console.ReadLine();

                        if (!int.TryParse(input, out var result)) {
                            Console.Clear();
                            Console.WriteLine("Invalid input, try again");
                            Console.ReadKey();
                            goto SUBCRIPTION;
                        }

                        Features feature = Features.None;
                        switch (result) {
                            case 1:
                                feature = Features.BasicMembership;
                                break;
                            case 2:
                                feature = Features.ProMembership;
                                break;
                            case 3:
                                feature = Features.EnterpriseMembership;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid input, try again");
                                Console.ReadKey();
                                goto SUBCRIPTION;
                        }

                        // check if same
                        Membership membership = SubscriptionManager.GetMembership(feature);
                        if (membership.is_user_own(account)) {
                            Console.Clear();
                            Console.WriteLine("You are already using this membership");
                            Console.ReadKey();
                            state = 0;
                            break;
                        }

                        // set
                        account.SetMembership(membership, 10);

                        Console.Clear();
                        Console.WriteLine("Membership changed, press 'any' key to continue");
                        Console.ReadKey();

                        state = 0;
                        break;
                    }

                    case 6: {
                        // log out
                        account = null;
                        state = 0;

                        Console.Clear();
                        Console.WriteLine("Logged out, press 'any' key to continue");
                        Console.ReadKey();
                        break;
                    }

                    case 7: {
                        // exit
                        loop = false;

                        Console.Clear();
                        Console.WriteLine("Exit, Program now exiting");
                        Console.WriteLine("Press any key to continue...");
                        break;
                    }

                    default: {
                        Console.WriteLine("Invalid State");
                        state = 0;
                        break;
                    }
                }
            }
        }   
    }
}
