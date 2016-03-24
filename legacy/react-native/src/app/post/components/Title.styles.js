import { StyleSheet } from 'react-native';

export default StyleSheet.create({
  item: {
    flex: 1,
    justifyContent: 'center',
    paddingBottom: 15,
    paddingTop: 10,
    paddingLeft: 10,
    paddingRight: 10,
  },
  title: {
    fontSize: 18,
    marginBottom: 2,
    textAlign: 'left',
  },
  sub: {
    marginTop: 8,
    flexDirection: 'row',
  },
  author: {
    fontSize: 12,
    textAlign: 'left',
    color: "#999",
  },
  date: {
    fontSize: 12,
    textAlign: 'left',
    marginLeft: 10,
    color: "#999",
  },
  separator: {
    flex: 1,
    height: 1,
    backgroundColor: '#ccc',
  },
});
