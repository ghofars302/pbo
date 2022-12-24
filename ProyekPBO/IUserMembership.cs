namespace ProyekPBO {
    internal interface IUserMembership {
        void SetMembership(Membership membership, int days);
        bool CheckSubscription();
    }
}
