namespace ProyekPBO {
    internal abstract class UserBase {
        public int UserId { set; get; }
        public string Username { set; get; }
        public Features Features { set; get; }
        public long MembershipEndAt { set; get; }
        public long MembershipDuration { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public Payment Payment { set; get; }
        public int Group { set; get; }
    }
}
