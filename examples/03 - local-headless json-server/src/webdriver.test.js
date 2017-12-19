const {Builder, By, Key, until} = require('selenium-webdriver')
const gecodriver = require('geckodriver');

test('make sure movies list has been rendered', (done) => {
    
    let driver = new Builder()
    .forBrowser('firefox')
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