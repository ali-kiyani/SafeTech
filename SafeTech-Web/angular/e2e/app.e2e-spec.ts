import { ForeSparkTemplatePage } from './app.po';

describe('ForeSpark App', function() {
  let page: ForeSparkTemplatePage;

  beforeEach(() => {
    page = new ForeSparkTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
