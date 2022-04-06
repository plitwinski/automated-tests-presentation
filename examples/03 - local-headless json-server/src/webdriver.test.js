import {Builder, By, Key, until} from 'selenium-webdriver'
import firefox from 'selenium-webdriver/firefox';
import gecodriver from 'geckodriver'; //this line is needed

let driver = null

beforeAll(() => {
  const binary = new firefox.Binary(firefox.Channel.NIGHTLY);
  binary.addArguments("-headless");
  driver = new Builder()
  .forBrowser('firefox')
  .setFirefoxOptions(new firefox.Options().setBinary(binary))
  .build();
}, 20000);

afterAll(() => driver.quit());

test('make sure movies list has been rendered', async () => {
  await driver.get('http://127.0.0.1:8000/src/index.html');
  driver.findElement(By.id('loadBtn')).sendKeys('webdriver', Key.ENTER);
  await driver.wait(until.elementsLocated(By.id('1')), 1000);
  const items = await driver.findElements(By.tagName('li'));
  expect(items.length).toBe(3);
}, 20000);