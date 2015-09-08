export default [
  {
    name: "Dashboard",
    url: "/",
    icon: "dashboard",
  },
  {
    name: "Security",
    icon: "lock",
    children: [
      {
        name: "Authorization",
        url: "/authorization",
      }
    ]
  }
];

