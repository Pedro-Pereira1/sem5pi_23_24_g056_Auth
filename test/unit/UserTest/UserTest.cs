using DDDSample1.Domain.Shared;
using RobDroneGoAuth.Domain.Users;

namespace UserTest;

[TestClass]
public class UserTest
{

    [TestMethod]
    [DataRow("Jose!")]
    [DataRow("Jose@")]
    [DataRow("Jose1")]
    public void Check_Invalid_Name(string value)
    {
        Assert.ThrowsException<BusinessRuleValidationException>(() => Name.Create(value));
    }    

    [TestMethod]
    [DataRow("Jose Gouveia")]
    public void Check_Valid_Name(string value)
    {
        Name name = Name.Create(value);
        Assert.AreEqual(value, name.NameString);
    }    

    [TestMethod]
    [DataRow("")]
    [DataRow("1211089isep.ipp.pt")]
    [DataRow("1211089@")]
    [DataRow("1211089@isep.pt")]
    public void Check_Invalid_Email(string value)
    {
        Assert.ThrowsException<BusinessRuleValidationException>(() => Email.Create(value));
    }

    [TestMethod]
    [DataRow("1211089@isep.ipp.pt")]
    public void Check_Valid_Email(string value)
    {
        Email email = Email.Create(value);
        Assert.AreEqual(value, email.Value);
    }

    [TestMethod]
    [DataRow("1211089")]
    [DataRow("12110890")]
    [DataRow("1211089A")]
    [DataRow("1211089a")]
    [DataRow("121108aA")]
    [DataRow("121108a0!")]
    [DataRow("12110890!")]
    [DataRow("1211089A!")]
    public void Check_Invalid_Password(string value)
    {
        Assert.ThrowsException<BusinessRuleValidationException>(() => Password.Create(value));
    }

    [TestMethod]
    [DataRow("1211089aA!")]
    public void Check_Valid_Password(string value)
    {
        Password password = Password.Create(value);
        Assert.AreEqual(value, password.PasswordString);
    }

    [TestMethod]
    [DataRow("93059772")]
    public void Check_Invalid_PhoneNumber(string value)
    {
        Assert.ThrowsException<BusinessRuleValidationException>(() => PhoneNumber.Create(value));
    }

    [TestMethod]
    [DataRow("930597721")]
    public void Check_Valid_PhoneNumber(string value)
    {
        PhoneNumber phoneNumber = PhoneNumber.Create(value);
        Assert.AreEqual(value, phoneNumber.Number);
    }

    [TestMethod]
    [DataRow("29008876")]
    public void Check_Invalid_TaxPayerNumber(string value)
    {
        Assert.ThrowsException<BusinessRuleValidationException>(() => PhoneNumber.Create(value));
    }

    [TestMethod]
    [DataRow("290088763")]
    public void Check_Valid_TaxPayerNumber(string value)
    {
        TaxPayerNumber taxPayerNumber = TaxPayerNumber.Create(value);
        Assert.AreEqual(value, taxPayerNumber.Number);
    }

    [TestMethod]
    [DataRow("Manager")]
    public void Check_Invalid_Role(string value)
    {
        Assert.ThrowsException<BusinessRuleValidationException>(() => Role.Create(value));
    }

    [TestMethod]
    [DataRow("Admin")]
    [DataRow("CampusManager")]
    [DataRow("FleetManager")]
    [DataRow("TaskManager")]
    [DataRow("Utente")]
    public void Check_Valid_Role(string value)
    {
        Role role = Role.Create(value);
        Assert.AreEqual(value, role.Value);
    }

    [TestMethod]
    public void Check_Invalid_User()
    {
        string name = "Jose Gouveia";
        string email = "1211089isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";
        string role = "Utente";

        Assert.ThrowsException<BusinessRuleValidationException>(() => User.Create(name, email, taxPayerNumber, phoneNumber, password, role));
    }

    [TestMethod]
    public void Check_Valid_User()
    {
        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";
        string role = "Utente";

        User user = User.Create(name, email, taxPayerNumber, phoneNumber, password, role);
        Assert.AreEqual(name, user.Name.NameString);
        Assert.AreEqual(email, user.Id.Value);
        Assert.AreEqual(phoneNumber, user.PhoneNumber.Number);
        Assert.AreEqual(taxPayerNumber, user.TaxPayerNumber.Number);
        Assert.AreEqual(password, user.Password.PasswordString);
    }
}