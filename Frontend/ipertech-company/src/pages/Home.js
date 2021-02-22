import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import About from "../components/About";
import Teaser from "../components/Teaser";
import { fetchPromotions } from "../redux/actions/promotionsActions/actionCreators";
import { fetchNews } from "../redux/actions/newsActions/actionCreators";
import Advert from "../components/Advert";

const Home = () => {
  const promotions = useSelector((state) => state.promotions);
  const news = useSelector((state) => state.news);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchPromotions());
  }, []);

  useEffect(() => {
    dispatch(fetchNews());
  }, []);

  return (
    <>
      <Teaser slider data={promotions} />
      <Teaser data={news} />
      <Advert />
      <About />
    </>
  );
};

export default Home;
