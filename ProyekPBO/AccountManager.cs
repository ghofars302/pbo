using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class AccountManager {
        public List<Account> accounts;

        public AccountManager() {
            accounts = new List<Account>();
        }

        public void LoadDatabase() {
            if (File.Exists("./users.csv")) {
                string contents = File.ReadAllText("./users.csv");

                string[] lines = contents.Split('\n');
                for (int i = 1; i < lines.Length; i++) { // skip the first index
                    string[] rows = lines[i].Split(',');
                    if (rows.Length < 7) {
                        Console.WriteLine("[CSV Reader] Syntax error at line: " + i + ", it expect 7 columns or more but got " + rows.Length);
                        continue;
                    }

                    int id = int.Parse(rows[0]);
                    string username = rows[1];
                    string password = rows[2];
                    string email = rows[3];
                    string address = rows[4];
                    int subscriptionType = int.Parse(rows[5]);
                    int duration = int.Parse(rows[6]);

                    Features feature;
                    switch (subscriptionType) {
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
                            throw new Exception("Invalid subscription type");
                    }

                    Account acc = new Account(id, username, password, email, address);
                    acc.SetMembership(SubscriptionManager.GetMembership(feature), duration);
                    accounts.Add(acc);
                }
            }
        }

        private string WrapComma(string input) {
            if (input.Contains(',')) {
                return "\"" + input.Replace("\"", "\"\"") + "\"";
            }

            return input;
        }

        public void SaveDatabase() {
            string toSave = "id,username,password,email,address,subscription_type,duration\n";
            string format = "{0},{1},{2},{3},{4},{5},{6}";

            foreach (Account acc in accounts) {
                string username = WrapComma(acc.Username);
                string password = WrapComma(acc.Password);
                string email = WrapComma(acc.Email);
                string address = WrapComma(acc.Address);
                int subscriptionType = (int)acc.Features;
                int duration = (int)acc.MembershipDuration;

                toSave += string.Format(format, acc.UserId, username, password, email, address, subscriptionType, duration) + "\n";
            }

            File.WriteAllText("./users.csv", toSave);
        }

        public Account GetAccount(string username) {
            foreach (Account acc in accounts) {
                if (acc.Username == username) {
                    return acc;
                }
            }

            return null;
        }

        public void AddAccount(Account account) {
            accounts.Add(account);
        }
    }
}
