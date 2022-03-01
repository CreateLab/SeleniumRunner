using System;
using System.Threading;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using Xunit;

namespace SeleniumTest;

public class MainTest
{
	public MainTest()
	{
	}

	[Fact(DisplayName = "Тест регистрации")]
	public void SignInTest()
	{
		FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Users\f98f9\Downloads", "geckodriver.exe");
		var driver = new FirefoxDriver(service);

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

		usernameInput.SendKeys("username" + r.Next());
		passwordInput.SendKeys("password" + r.Next());

		var firstNameText = "firstName" + r.Next();
		firstName.SendKeys(firstNameText);
		var lastNameText = "lastName" + r.Next();

		lastName.SendKeys(lastNameText);

		middleName.SendKeys("middleName" + r.Next());

		var createButton = driver.FindElement(By.Id("sign-up-button"));
		createButton.Click();
		Thread.Sleep(1000);

		var nameButton = driver.FindElement(By.Id("name-button"));
		var text = nameButton.Text;

		text.Should()
			.Be(firstNameText + " " + lastNameText);

		driver.Close();
		driver.Dispose();
	}

	[Fact(DisplayName = "Тест авторизации")]
	public void SignUpTest()
	{
		FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Users\f98f9\Downloads", "geckodriver.exe");
		var driver = new FirefoxDriver(service);

		driver.Navigate()
			.GoToUrl("http://localhost:4200/");

		var username = driver.FindElement(By.Id("username"));
		var password = driver.FindElement(By.Id("password"));

		username.SendKeys("sttimort");
		password.SendKeys("123456");

		var singInButton = driver.FindElement(By.Id("sign-in-button"));
		singInButton.Click();

		Thread.Sleep(1000);
		var nameButton = driver.FindElement(By.Id("name-button"));
		var text = nameButton.Text;

		text.Should()
			.Be("Михаил Горбунов");

		driver.Close();
		driver.Dispose();
	}

	[Fact(DisplayName = "Тест Создания крестьянина")]
	public void CreatePeasantTest()
	{
		FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Users\f98f9\Downloads", "geckodriver.exe");
		var driver = new FirefoxDriver(service);

		driver.Navigate()
			.GoToUrl("http://localhost:4200/");

		var username = driver.FindElement(By.Id("username"));
		var password = driver.FindElement(By.Id("password"));

		username.SendKeys("sttimort");
		password.SendKeys("123456");

		var singInButton = driver.FindElement(By.Id("sign-in-button"));
		singInButton.Click();

		Thread.Sleep(1000);

		var peasant = driver.FindElement(By.XPath(@"//span[contains(text(),'Крепостные')]"));


		var action1 = new Actions(driver);
		action1.MoveToElement(peasant);
		action1.Perform();
		peasant
			.Click();

		Thread.Sleep(1000);

		var findElement = driver.FindElement(By.XPath(@"//span[contains(text(),'Регистрация владения')]"));
		var action2 = new Actions(driver);
		action2.MoveToElement(findElement);
		action2.Perform();
		Thread.Sleep(1000);

		driver.FindElement(By.XPath(@"//span[contains(text(),'Создать заявку на регистрацию владения')]"))
			.Click();

		Thread.Sleep(500);


		var firstName = driver.FindElement(By.Id("firstName"));
		var lastName = driver.FindElement(By.Id("lastName"));
		var middleName = driver.FindElement(By.Id("middleName"));
		var birthPlace = driver.FindElement(By.Id("placeBirth"));
		var sex = driver.FindElement(By.Id("sex"));
		var owner = driver.FindElement(By.Id("owner"));
		var button = driver.FindElement(By.Id("send-button"));

		firstName.SendKeys("имя");
		lastName.SendKeys("фамилия");
		middleName.SendKeys("отчество");
		sex.SendKeys("пол");
		owner.SendKeys("причина владения");
		birthPlace.SendKeys("место рождения");
		button.Click();
		Thread.Sleep(500);



		action1.MoveToElement(peasant);
		action1.Perform();
		peasant
			.Click();
		action2.MoveToElement(findElement);
		action2.Perform();

		driver.FindElement(By.XPath(@"//span[contains(text(),'Рассмотрение заявок на регистрацию владения')]"))
			.Click();

		var text = driver.FindElement(By.XPath(@"//label[contains(text(),'Крестьянин')]/following-sibling::*")).Text;

		text.Should()
			.Be("имя фамилия отчество (пол, м.р. – место рождения)");
		driver.Close();
		driver.Dispose();
	}


	[Fact(DisplayName = "Тест на администратора")]
	public void AdminTest()
	{
		FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Users\f98f9\Downloads", "geckodriver.exe");
		var driver = new FirefoxDriver(service);

		driver.Navigate()
			.GoToUrl("http://localhost:4200/");

		var username = driver.FindElement(By.Id("username"));
		var password = driver.FindElement(By.Id("password"));

		username.SendKeys("111");
		password.SendKeys("111111");

		var singInButton = driver.FindElement(By.Id("sign-in-button"));
		singInButton.Click();

		Thread.Sleep(1000);


		Action act = () => driver.FindElement(By.XPath(@"//span[contains(text(),'Администрирование')]"));

		act.Should()
			.Throw<NoSuchElementException>();

		driver.Close();
		driver.Dispose();
	}
}