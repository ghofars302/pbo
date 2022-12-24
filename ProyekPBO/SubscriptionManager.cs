using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal static class SubscriptionManager {
        
        // Di c# nama nya itu CONST, tapi di java namanya FINAL
        public const Features default_membership = Features.None;

        public static Dictionary<Membership, string> List = new Dictionary<Membership, string>() {
            { new BasicMembership(), "Basic Membership" },
            { new ProMembership(), "Pro Membership" },
            { new EnterpriseMembership(), "Enterprise Membership" }
        };

        public static Features is_user_own(Account account, Features feature) {
            if (account.Features == feature) {
                return feature;
            }
            
            return Features.None;
        }

        public static Membership GetMembership(Features feature) {
            switch (feature) {
                case Features.BasicMembership:
                    return new BasicMembership();
                case Features.ProMembership:
                    return new ProMembership();
                case Features.EnterpriseMembership:
                    return new EnterpriseMembership();
                default:
                    return new Membership();
            }
        }
    }
}
