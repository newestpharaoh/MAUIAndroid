namespace CommonLibraryCoreMaui
{
    public interface IBottomNavBarIcon
    {
        BottomNavBarIcon SelectedBottomNavBarIcon { get; }
    }

    public interface IPatientBottomNavBarIcon
    {
        PatientBottomNavBarIcon SelectedBottomNavBarIcon { get; }
    }

    public struct PatientBottomNavBarIcon
    {
        public BottomNavBarIcon SelectedBottomNavBarIcon { get; set; }
        public bool Clickable { get; set; }
    }

    public enum BottomNavBarIcon
    {
        None,
        Home,
        VisitsHistory,
        Family,
        MedicalHistory,
        Account
    }

    public interface IBackToProviderSelection
    {
        void Back();
    }
}
