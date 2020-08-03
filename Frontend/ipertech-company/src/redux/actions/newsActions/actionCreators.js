import { FETCH_NEWS, INSERT_NEWS, DELETE_NEWS } from "./actionTypes";
import { getNews, postNews, deleteNew } from "../../../services/newsService";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";

export function fetchNews() {
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

export function insertNews(news) {
  return (dispatch) => {
    postNews(news).then((data) => {
      const { data: newNews } = data.success;
      console.log(data);
      dispatch({
        type: INSERT_NEWS,
        newNews,
      });
    });
  };
}

export function deleteNews(news) {
  return (dispatch) => {
    deleteNew(news.notificationId, news.notificationTypeId).then((data) => {
      const { message } = data.success;

      dispatch({
        type: DELETE_NEWS,
        notificationId: news.notificationId,
        notificationTypeId: news.notificationTypeId,
      });

      dispatch(fetchNews());

      return dispatch(
        addNotification({
          type: notificationTypes.INFO,
          message,
          duration: 5000,
        })
      );
    });
  };
}
