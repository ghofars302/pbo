using System;

namespace ProyekPBO {
    internal class Account : UserBase, IUserMembership {
        public static string ClassName = "Account";
        public bool IsInGroup => Group != 0;

        public Account(int user_id, string username, string password, string email, string address) {
            UserId = user_id;
            Username = username;
            Password = password;
            Email = email;
            Address = address;
        }

        public Account(int user_id, string username, string email, string address, Payment payment) {
            UserId = user_id;
            Username = username;
            Email = email;
            Address = address;
            Payment = payment;
        }

        public Account(int user_id, string username, string email, string address, Payment payment, int group) {
            UserId = user_id;
            Username = username;
            Email = email;
            Address = address;
            Payment = payment;
            Group = group;
        }

        public virtual void SetMembership(Membership membership, int days) {
            Features = membership.Feature;
            MembershipEndAt = DateTime.Now.Ticks + (days * 24 * 60 * 60 * 10000000);
            MembershipDuration = days;
        }

        public virtual bool CheckSubscription() {
            DateTime now = DateTime.Now;
            DateTime expireAt = new DateTime(MembershipEndAt);

            TimeSpan span = expireAt - now;
            int days = span.Days;

            return days < -1;
        }

        public virtual void JoinGroup(int guid) {
            Group = guid;
        }
        
        public static string Sformat(string format, params object[] objects) {
            return string.Format(format, objects);
        }
    }
}
