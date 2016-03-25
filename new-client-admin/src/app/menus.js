export default [
  {
    name: 'Home',
    url: '/',
    icon: 'home',
  },
  {
    name: 'router-example',
    icon: 'database',
    children: [
      {
        name: 'Page1',
        url: '/router-example/page1',
      },
      {
        name: 'Page2',
        url: '/router-example/page2',
      }
    ],
  },
  {
    name: 'ModuleB',
    icon: 'lock',
    children: [
      {
        name: 'Page1',
        url: '/module-b/page1',
      },
      {
        name: 'Page2',
        url: '/module-b/page2',
      },
      {
        name: 'Page3',
        url: '/module-b/page3',
      }
    ]
  }
];

