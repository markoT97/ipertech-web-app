import { FETCH_NEWS } from "./actionTypes";
import { getNews } from "../../../services/newsService";

function fetchNews() {
  return (dispatch) => {
    getNews().then((data) => {
      const { news } = data.success;
      dispatch({
        type: FETCH_NEWS,
        news,
      });
    });
  };
}
export default fetchNews;
