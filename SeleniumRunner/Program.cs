// See https://aka.ms/new-console-template for more information

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V96.Audits;
using OpenQA.Selenium.Firefox;

FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Users\f98f9\Downloads", "geckodriver.exe");
IWebDriver driver = new FirefoxDriver(service);

driver.Navigate()
	.GoToUrl("http://localhost:4200/");

Thread.Sleep(1000);
var checkIn = driver.FindElement(By.Id("p-tabpanel-1-label"));
checkIn.Click();
Thread.Sleep(1000);

var usernameInput = driver.FindElement(By.Id("username-input"));
var passwordInput = driver.FindElement(By.Id("password-input"));
var firstName = driver.FindElement(By.Id("first-name"));
var lastName = driver.FindElement(By.Id("last-name"));
var middleName = driver.FindElement(By.Id("middle-name"));

var r = new Random();

usernameInput.SendKeys("username"+r.Next());
passwordInput.SendKeys("password" + r.Next());

firstName.SendKeys("firstName" + r.Next());

lastName.SendKeys("lastName" + r.Next());

middleName.SendKeys("middleName" + r.Next());

var createButton = driver.FindElement(By.Id("sign-up-button"));
createButton.Click();

driver.Close();

Console.WriteLine("Тест на регистрацию пройден успешно");

driver.Navigate()
	.GoToUrl("http://localhost:4200/");

var username = driver.FindElement(By.Id("username"));
var password = driver.FindElement(By.Id("password"));

username.SendKeys("sttimort");
password.SendKeys("123456");

var singInButton = driver.FindElement(By.Id("sign-in-button"));
singInButton.Click();