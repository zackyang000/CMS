export default [
  {
    name: "Home",
    url: "/",
    icon: "home",
  },
  {
    name: "ModuleA",
    icon: "database",
    children: [
      {
        name: "Page1",
        url: "/module-a/page1",
      },
      {
        name: "Page2",
        url: "/module-a/page2",
      }
    ],
  },
  {
    name: "ModuleB",
    icon: "lock",
    children: [
      {
        name: "Page1",
        url: "/module-b/page1",
      },
      {
        name: "Page2",
        url: "/module-b/page2",
      },
      {
        name: "Page3",
        url: "/module-b/page3",
      }
    ]
  }
];

