const {Builder, By, Key, until} = require('selenium-webdriver')
const firefox = require('selenium-webdriver/firefox');
const gecodriver = require('geckodriver');

test('make sure movies list has been rendered', (done) => {
    
    const binary = new firefox.Binary(firefox.Channel.NIGHTLY);
    binary.addArguments("-headless");

    const driver = new Builder()
    .forBrowser('firefox')
    .setFirefoxOptions(new firefox.Options().setBinary(binary))
    .build();

    driver.get('http://127.0.0.1:8000/src/index.html').then(() => {
        driver.findElement(By.id('loadBtn')).sendKeys('webdriver', Key.ENTER)
        driver.wait(until.elementsLocated(By.id('1')), 1000).then(() => {
            driver.findElements(By.tagName('li')).then(items => {
                expect(items.length).toBe(3)
                driver.quit()
                done()
            })
        })
    });
});