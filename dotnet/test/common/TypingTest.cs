// <copyright file="TypingTest.cs" company="Selenium Committers">
// Licensed to the Software Freedom Conservancy (SFC) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The SFC licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
// </copyright>

using NUnit.Framework;
using OpenQA.Selenium.Environment;
using System;
using System.Runtime.InteropServices;

namespace OpenQA.Selenium
{
    [TestFixture]
    public class TypingTest : DriverTestFixture
    {
        [Test]
        public void ShouldFireKeyPressEvents()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("a");

            IWebElement result = driver.FindElement(By.Id("result"));
            string text = result.Text;
            Assert.That(text, Does.Contain("press:"));
        }

        [Test]
        public void ShouldFireKeyDownEvents()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("I");

            IWebElement result = driver.FindElement(By.Id("result"));
            string text = result.Text;
            Assert.That(text, Does.Contain("down:"));
        }

        [Test]
        public void ShouldFireKeyUpEvents()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("a");

            IWebElement result = driver.FindElement(By.Id("result"));
            string text = result.Text;
            Assert.That(text, Does.Contain("up:"));
        }

        [Test]
        public void ShouldTypeLowerCaseLetters()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("abc def");

            Assert.AreEqual("abc def", keyReporter.GetAttribute("value"));
        }

        [Test]
        public void ShouldBeAbleToTypeCapitalLetters()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("ABC DEF");

            Assert.AreEqual("ABC DEF", keyReporter.GetAttribute("value"));
        }

        [Test]
        public void ShouldBeAbleToTypeQuoteMarks()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("\"");

            Assert.AreEqual("\"", keyReporter.GetAttribute("value"));
        }

        [Test]
        public void ShouldBeAbleToTypeTheAtCharacter()
        {
            // simon: I tend to use a US/UK or AUS keyboard layout with English
            // as my primary language. There are consistent reports that we're
            // not handling i18nised keyboards properly. This test exposes this
            // in a lightweight manner when my keyboard is set to the DE mapping
            // and we're using IE.

            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("@");

            Assert.AreEqual("@", keyReporter.GetAttribute("value"));
        }

        [Test]
        public void ShouldBeAbleToMixUpperAndLowerCaseLetters()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("me@eXample.com");

            Assert.AreEqual("me@eXample.com", keyReporter.GetAttribute("value"));
        }

        [Test]
        public void ArrowKeysShouldNotBePrintable()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys(Keys.ArrowLeft);

            Assert.AreEqual(string.Empty, keyReporter.GetAttribute("value"));
        }

        [Test]
        public void ShouldBeAbleToUseArrowKeys()
        {
            driver.Url = javascriptPage;

            IWebElement keyReporter = driver.FindElement(By.Id("keyReporter"));
            keyReporter.SendKeys("Tet" + Keys.ArrowLeft + "s");

            Assert.AreEqual("Test", keyReporter.GetAttribute("value"));
        }

        [Test]
        public void WillSimulateAKeyUpWhenEnteringTextIntoInputElements()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyUp"));
            element.SendKeys("I like cheese");

            IWebElement result = driver.FindElement(By.Id("result"));
            Assert.AreEqual("I like cheese", result.Text);
        }

        [Test]
        public void WillSimulateAKeyDownWhenEnteringTextIntoInputElements()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyDown"));
            element.SendKeys("I like cheese");

            IWebElement result = driver.FindElement(By.Id("result"));
            // Because the key down gets the result before the input element is
            // filled, we're a letter short here
            Assert.AreEqual("I like chees", result.Text);
        }

        [Test]
        public void WillSimulateAKeyPressWhenEnteringTextIntoInputElements()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyPress"));
            element.SendKeys("I like cheese");

            IWebElement result = driver.FindElement(By.Id("result"));
            // Because the key down gets the result before the input element is
            // filled, we're a letter short here
            Assert.AreEqual("I like chees", result.Text);
        }

        [Test]
        public void WillSimulateAKeyUpWhenEnteringTextIntoTextAreas()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyUpArea"));
            element.SendKeys("I like cheese");

            IWebElement result = driver.FindElement(By.Id("result"));
            Assert.AreEqual("I like cheese", result.Text);
        }

        [Test]
        public void WillSimulateAKeyDownWhenEnteringTextIntoTextAreas()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyDownArea"));
            element.SendKeys("I like cheese");

            IWebElement result = driver.FindElement(By.Id("result"));
            // Because the key down gets the result before the input element is
            // filled, we're a letter short here
            Assert.AreEqual("I like chees", result.Text);
        }

        [Test]
        public void WillSimulateAKeyPressWhenEnteringTextIntoTextAreas()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyPressArea"));
            element.SendKeys("I like cheese");

            IWebElement result = driver.FindElement(By.Id("result"));
            // Because the key down gets the result before the input element is
            // filled, we're a letter short here
            Assert.AreEqual("I like chees", result.Text);
        }

        [Test]
        public void ShouldFireFocusKeyEventsInTheRightOrder()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("theworks"));

            element.SendKeys("a");
            Assert.AreEqual("focus keydown keypress keyup", result.Text.Trim());
        }

        [Test]
        public void ShouldReportKeyCodeOfArrowKeys()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys(Keys.ArrowDown);
            CheckRecordedKeySequence(result, 40);

            element.SendKeys(Keys.ArrowUp);
            CheckRecordedKeySequence(result, 38);

            element.SendKeys(Keys.ArrowLeft);
            CheckRecordedKeySequence(result, 37);

            element.SendKeys(Keys.ArrowRight);
            CheckRecordedKeySequence(result, 39);

            // And leave no rubbish/printable keys in the "keyReporter"
            Assert.AreEqual(string.Empty, element.GetAttribute("value"));
        }

        [Test]
        public void ShouldReportKeyCodeOfArrowKeysUpDownEvents()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys(Keys.ArrowDown);
            string text = result.Text.Trim();
            Assert.That(text, Does.Contain("down: 40"));
            Assert.That(text, Does.Contain("up: 40"));

            element.SendKeys(Keys.ArrowUp);
            text = result.Text.Trim();
            Assert.That(text, Does.Contain("down: 38"));
            Assert.That(text, Does.Contain("up: 38"));

            element.SendKeys(Keys.ArrowLeft);
            text = result.Text.Trim();
            Assert.That(text, Does.Contain("down: 37"));
            Assert.That(text, Does.Contain("up: 37"));

            element.SendKeys(Keys.ArrowRight);
            text = result.Text.Trim();
            Assert.That(text, Does.Contain("down: 39"));
            Assert.That(text, Does.Contain("up: 39"));

            // And leave no rubbish/printable keys in the "keyReporter"
            Assert.AreEqual(string.Empty, element.GetAttribute("value"));
        }

        [Test]
        public void NumericNonShiftKeys()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            string numericLineCharsNonShifted = "`1234567890-=[]\\;,.'/42";
            element.SendKeys(numericLineCharsNonShifted);

            Assert.AreEqual(numericLineCharsNonShifted, element.GetAttribute("value"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/646")]
        public void NumericShiftKeys()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            string numericShiftsEtc = "~!@#$%^&*()_+{}:\"<>?|END~";
            element.SendKeys(numericShiftsEtc);

            Assert.AreEqual(numericShiftsEtc, element.GetAttribute("value"));
            string text = result.Text.Trim();
            Assert.That(text, Does.Contain(" up: 16"));
        }

        [Test]
        public void LowerCaseAlphaKeys()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            String lowerAlphas = "abcdefghijklmnopqrstuvwxyz";
            element.SendKeys(lowerAlphas);

            Assert.AreEqual(lowerAlphas, element.GetAttribute("value"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/646")]
        public void UppercaseAlphaKeys()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            String upperAlphas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            element.SendKeys(upperAlphas);

            Assert.AreEqual(upperAlphas, element.GetAttribute("value"));
            string text = result.Text.Trim();
            Assert.That(text, Does.Contain(" up: 16"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/646")]
        public void AllPrintableKeys()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            String allPrintable =
                "!\"#$%&'()*+,-./0123456789:;<=>?@ ABCDEFGHIJKLMNO" +
                "PQRSTUVWXYZ [\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
            element.SendKeys(allPrintable);

            Assert.AreEqual(allPrintable, element.GetAttribute("value"));
            string text = result.Text.Trim();
            Assert.That(text, Does.Contain(" up: 16"));
        }

        [Test]
        public void ArrowKeysAndPageUpAndDown()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("a" + Keys.Left + "b" + Keys.Right +
                             Keys.Up + Keys.Down + Keys.PageUp + Keys.PageDown + "1");
            Assert.AreEqual("ba1", element.GetAttribute("value"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/2015")]
        public void HomeAndEndAndPageUpAndPageDownKeys()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("abc" + HomeKey() + "0" + Keys.Left + Keys.Right +
                             Keys.PageUp + Keys.PageDown + EndKey() + "1" + HomeKey() +
                             "0" + Keys.PageUp + EndKey() + "111" + HomeKey() + "00");
            Assert.AreEqual("0000abc1111", element.GetAttribute("value"));
        }

        [Test]
        public void DeleteAndBackspaceKeys()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("abcdefghi");
            Assert.AreEqual("abcdefghi", element.GetAttribute("value"));

            element.SendKeys(Keys.Left + Keys.Left + Keys.Delete);
            Assert.AreEqual("abcdefgi", element.GetAttribute("value"));

            element.SendKeys(Keys.Left + Keys.Left + Keys.Backspace);
            Assert.AreEqual("abcdfgi", element.GetAttribute("value"));
        }

        [Test]
        public void SpecialSpaceKeys()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("abcd" + Keys.Space + "fgh" + Keys.Space + "ij");
            Assert.AreEqual("abcd fgh ij", element.GetAttribute("value"));
        }

        [Test]
        public void NumberpadKeys()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("abcd" + Keys.Multiply + Keys.Subtract + Keys.Add +
                             Keys.Decimal + Keys.Separator + Keys.NumberPad0 + Keys.NumberPad9 +
                             Keys.Add + Keys.Semicolon + Keys.Equal + Keys.Divide +
                             Keys.NumberPad3 + "abcd");
            Assert.AreEqual("abcd*-+.,09+;=/3abcd", element.GetAttribute("value"));
        }

        [Test]
        public void FunctionKeys()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("FUNCTION" + Keys.F8 + "-KEYS" + Keys.F8);
            element.SendKeys("" + Keys.F8 + "-TOO" + Keys.F8);
            Assert.AreEqual("FUNCTION-KEYS-TOO", element.GetAttribute("value"));
        }

        [Test]
        public void ShiftSelectionDeletes()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("abcd efgh");
            Assert.AreEqual(element.GetAttribute("value"), "abcd efgh");

            //Could be chord problem
            element.SendKeys(Keys.Shift + Keys.Left + Keys.Left + Keys.Left);
            element.SendKeys(Keys.Delete);
            Assert.AreEqual("abcd e", element.GetAttribute("value"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/646")]
        public void ChordControlHomeShiftEndDelete()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("!\"#$%&'()*+,-./0123456789:;<=>?@ ABCDEFG");

            element.SendKeys(HomeKey());
            element.SendKeys("" + Keys.Shift + EndKey() + Keys.Delete);

            Assert.AreEqual(string.Empty, element.GetAttribute("value"));
            string text = result.Text.Trim();
            Assert.That(text, Does.Contain(" up: 16"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/2015")]
        public void ChordReverseShiftHomeSelectionDeletes()
        {
            driver.Url = javascriptPage;

            IWebElement result = driver.FindElement(By.Id("result"));
            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            element.SendKeys("done" + HomeKey());
            Assert.AreEqual("done", element.GetAttribute("value"));

            //Sending chords
            element.SendKeys("" + Keys.Shift + "ALL " + HomeKey());
            Assert.AreEqual("ALL done", element.GetAttribute("value"));

            element.SendKeys(Keys.Delete);
            Assert.AreEqual("done", element.GetAttribute("value"), "done");

            element.SendKeys("" + EndKey() + Keys.Shift + HomeKey());
            Assert.AreEqual("done", element.GetAttribute("value"));
            // Note: trailing SHIFT up here
            string text = result.Text.Trim();

            element.SendKeys("" + Keys.Delete);
            Assert.AreEqual(string.Empty, element.GetAttribute("value"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/2015")]
        public void ChordControlCutAndPaste()
        {
            driver.Url = javascriptPage;

            IWebElement element = driver.FindElement(By.Id("keyReporter"));

            String paste = "!\"#$%&'()*+,-./0123456789:;<=>?@ ABCDEFG";
            element.SendKeys(paste);
            Assert.AreEqual(paste, element.GetAttribute("value"));

            //Chords
            element.SendKeys("" + HomeKey() + Keys.Shift + EndKey());

            element.SendKeys(PrimaryModifier() + "x");
            Assert.AreEqual(string.Empty, element.GetAttribute("value"));

            element.SendKeys(PrimaryModifier() + "v");
            Assert.AreEqual(paste, element.GetAttribute("value"));

            element.SendKeys("" + Keys.Left + Keys.Left + Keys.Left +
                             Keys.Shift + EndKey());
            element.SendKeys(PrimaryModifier() + "x" + "v");
            Assert.AreEqual(paste, element.GetAttribute("value"));

            element.SendKeys(HomeKey());
            element.SendKeys(PrimaryModifier() + "v");
            element.SendKeys(PrimaryModifier() + "v" + "v");
            element.SendKeys(PrimaryModifier() + "v" + "v" + "v");
            Assert.AreEqual("EFGEFGEFGEFGEFGEFG" + paste, element.GetAttribute("value"));

            element.SendKeys("" + EndKey() + Keys.Shift + HomeKey() +
                             Keys.Null + Keys.Delete);
            Assert.AreEqual(element.GetAttribute("value"), string.Empty);
        }

        [Test]
        public void ShouldTypeIntoInputElementsThatHaveNoTypeAttribute()
        {
            driver.Url = formsPage;

            IWebElement element = driver.FindElement(By.Id("no-type"));

            element.SendKeys("Should Say Cheese");
            Assert.AreEqual("Should Say Cheese", element.GetAttribute("value"));
        }

        [Test]
        public void ShouldNotTypeIntoElementsThatPreventKeyDownEvents()
        {
            driver.Url = javascriptPage;

            IWebElement silent = driver.FindElement(By.Name("suppress"));

            silent.SendKeys("s");
            Assert.AreEqual(string.Empty, silent.GetAttribute("value"));
        }

        [Test]
        public void GenerateKeyPressEventEvenWhenElementPreventsDefault()
        {
            driver.Url = javascriptPage;

            IWebElement silent = driver.FindElement(By.Name("suppress"));
            IWebElement result = driver.FindElement(By.Id("result"));

            silent.SendKeys("s");
            string text = result.Text;
        }

        [Test]
        public void ShouldBeAbleToTypeOnAnEmailInputField()
        {
            driver.Url = formsPage;
            IWebElement email = driver.FindElement(By.Id("email"));
            email.SendKeys("foobar");
            Assert.AreEqual("foobar", email.GetAttribute("value"));
        }

        [Test]
        public void ShouldBeAbleToTypeOnANumberInputField()
        {
            driver.Url = formsPage;
            IWebElement numberElement = driver.FindElement(By.Id("age"));
            numberElement.SendKeys("33");
            Assert.AreEqual("33", numberElement.GetAttribute("value"));
        }

        [Test]
        public void ShouldThrowIllegalArgumentException()
        {
            driver.Url = formsPage;
            IWebElement email = driver.FindElement(By.Id("age"));
            Assert.That(() => email.SendKeys(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void CanSafelyTypeOnElementThatIsRemovedFromTheDomOnKeyPress()
        {
            driver.Url = EnvironmentManager.Instance.UrlBuilder.WhereIs("key_tests/remove_on_keypress.html");

            IWebElement input = driver.FindElement(By.Id("target"));
            IWebElement log = driver.FindElement(By.Id("log"));

            Assert.AreEqual("", log.GetAttribute("value"));

            input.SendKeys("b");
            string expected = "keydown (target)\nkeyup (target)\nkeyup (body)";
            Assert.AreEqual(expected, GetValueText(log));

            input.SendKeys("a");

            // Some drivers (IE, Firefox) do not always generate the final keyup event since the element
            // is removed from the DOM in response to the keypress (note, this is a product of how events
            // are generated and does not match actual user behavior).
            expected += "\nkeydown (target)\na pressed; removing";
            Assert.That(GetValueText(log), Is.EqualTo(expected).Or.EqualTo(expected + "\nkeyup (body)"));
        }

        [Test]
        public void CanClearNumberInputAfterTypingInvalidInput()
        {
            driver.Url = formsPage;
            IWebElement input = driver.FindElement(By.Id("age"));
            input.SendKeys("e");
            input.Clear();
            input.SendKeys("3");
            Assert.AreEqual("3", input.GetAttribute("value"));
        }

        //------------------------------------------------------------------
        // Tests below here are not included in the Java test suite
        //------------------------------------------------------------------
        [Test]
        [IgnoreBrowser(Browser.Firefox, "Browser does not automatically focus body element in frame")]
        public void TypingIntoAnIFrameWithContentEditableOrDesignModeSet()
        {
            driver.Url = richTextPage;

            driver.SwitchTo().Frame("editFrame");
            IWebElement element = driver.SwitchTo().ActiveElement();
            element.SendKeys("Fishy");

            driver.SwitchTo().DefaultContent();
            IWebElement trusted = driver.FindElement(By.Id("istrusted"));
            IWebElement id = driver.FindElement(By.Id("tagId"));

            Assert.That(trusted.Text, Is.EqualTo("[true]").Or.EqualTo("[n/a]").Or.EqualTo("[]"));
            Assert.That(id.Text, Is.EqualTo("[frameHtml]").Or.EqualTo("[theBody]"));
        }

        [Test]
        [IgnoreBrowser(Browser.Firefox, "Browser does not automatically focus body element in frame")]
        public void NonPrintableCharactersShouldWorkWithContentEditableOrDesignModeSet()
        {
            driver.Url = richTextPage;

            driver.SwitchTo().Frame("editFrame");
            IWebElement element = driver.SwitchTo().ActiveElement();

            //Chords
            element.SendKeys("Dishy" + Keys.Backspace + Keys.Left + Keys.Left);
            element.SendKeys(Keys.Left + Keys.Left + "F" + Keys.Delete + EndKey() + "ee!");

            Assert.AreEqual(element.Text, "Fishee!");
        }

        [Test]
        public void ShouldBeAbleToTypeIntoEmptyContentEditableElement()
        {
            driver.Url = readOnlyPage;
            IWebElement editable = driver.FindElement(By.Id("content-editable"));

            editable.Clear();
            editable.SendKeys("cheese"); // requires focus on OS X

            Assert.AreEqual("cheese", editable.Text);
        }

        [Test]
        // [IgnoreBrowser(Browser.Chrome, "Driver prepends text in contentEditable")]
        // [IgnoreBrowser(Browser.Edge, "Driver prepends text in contentEditable")]
        [IgnoreBrowser(Browser.Firefox, "https://github.com/mozilla/geckodriver/issues/2015")]
        public void ShouldBeAbleToTypeIntoContentEditableElementWithExistingValue()
        {
            driver.Url = readOnlyPage;
            IWebElement editable = driver.FindElement(By.Id("content-editable"));

            string initialText = editable.Text;
            editable.SendKeys(", edited");

            Assert.AreEqual(initialText + ", edited", editable.Text);
        }

        [Test]
        [NeedsFreshDriver(IsCreatedAfterTest = true)]
        public void ShouldBeAbleToTypeIntoTinyMCE()
        {
            driver.Url = EnvironmentManager.Instance.UrlBuilder.WhereIs("tinymce.html");
            driver.SwitchTo().Frame("mce_0_ifr");

            IWebElement editable = driver.FindElement(By.Id("tinymce"));

            editable.Clear();
            editable.SendKeys("cheese"); // requires focus on OS X

            Assert.AreEqual("cheese", editable.Text);
        }

        private string GetValueText(IWebElement el)
        {
            // Standardize on \n and strip any trailing whitespace.
            return el.GetAttribute("value").Replace("\r\n", "\n").Trim();
        }

        private void CheckRecordedKeySequence(IWebElement element, int expectedKeyCode)
        {
            string withKeyPress = string.Format("down: {0} press: {0} up: {0}", expectedKeyCode);
            string withoutKeyPress = string.Format("down: {0} up: {0}", expectedKeyCode);
            Assert.That(element.Text.Trim(), Is.AnyOf(withKeyPress, withoutKeyPress));
        }

        private string PrimaryModifier()
        {
            return (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) ? Keys.Command : Keys.Control;
        }

        private string HomeKey()
        {
            return (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) ? Keys.Up : Keys.Home;
        }

        private string EndKey()
        {
            return (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) ? Keys.Down : Keys.End;
        }
    }
}
