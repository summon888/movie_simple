import { Box, Button, Card, CardContent, CardMedia, Container, IconButton, Typography, makeStyles, useTheme } from '@mui/material';
import ThumbUpOffAltIcon from '@mui/icons-material/ThumbUpOffAlt';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import { Label } from '@mui/icons-material';
import { useEffect, useState } from 'react';
import { useAppSelector } from '../redux/store';
import { selectUser } from '../redux/features/userSlice';
import InfiniteScroll from 'react-infinite-scroll-component';
import { useGetMovieMutation, useLikeMutation } from '../redux/api/movieApi';
import { toast } from 'react-toastify';

const HomePage = () => {
  //const { data, isSuccess, isLoading } = useGetMovieQuery({ skip: 0, take: 10 });
  const theme = useTheme();
  const [items, setItems] = useState([])
  const [page, setPage] = useState(0)

  const [getMovie, { data, isLoading, isSuccess }] = useGetMovieMutation();
  const [like] = useLikeMutation();

  useEffect(() => {
    getMovie({ page }).then((result: any) => {
      if (!result.error){
        const new_movies = result.data?.data;
        const need_add: Array<any> = [];
        for (const movie of new_movies) {
          if (items.findIndex((a: any) => a.id == movie.id) > -1)
            continue;
  
          need_add.push(movie);
        }
        setItems([...items, ...need_add as never[]])
      }
    });
  }, [page]);

  const updateLikeField = (id: string) => {
    const movie: any = items.find((a: any) => a.id === id);
    setItems((prevItems: any) =>
      prevItems.map((item: any) =>
        item.id === id ? { ...item, ['totalLike']: movie.totalLike + 1 } : item
      )
    );
  };

  const updateMovieDisLike = (id: string) => {
    const movie: any = items.find((a: any) => a.id === id);
    setItems((prevItems: any) =>
      prevItems.map((item: any) =>
        item.id === id ? { ...item, ['totalDislikes']: movie.totalDislikes + 1 } : item
      )
    );
  };

  const handleLike = (id: string) => {
    like({ movieId: id, isLike: true }).then((result: any) => {
      if (result.data?.success) {
        updateLikeField(id);
        toast.success('like successfully');
      } else {
        toast.error(JSON.stringify(result.error));
      }
    })
  };

  const handleDislike = (id: any) => {
    like({ movieId: id, isLike: false }).then((result: any) => {
      if (result.data?.success) {
        toast.success('dislike successfully');
        updateMovieDisLike(id);
      } else {
        toast.error(JSON.stringify(result.error));
      }
    })
  };


  const onScroll = () => {
    const scrollTop = document.documentElement.scrollTop
    const scrollHeight = document.documentElement.scrollHeight
    const clientHeight = document.documentElement.clientHeight

    if (scrollTop + clientHeight >= scrollHeight) {
      setPage(page + 1)
    }
  }

  useEffect(() => {
    window.addEventListener('scroll', onScroll)
    return () => window.removeEventListener('scroll', onScroll)
  }, [items])

  return (
    <Container fixed style={{ paddingBottom: '60px', paddingTop: '50px' }}>
      <div>
        {items.map((movie: any, index) => (
          <div key={index}>
            <Card style={{ border: '1px solid #ccc', marginTop: "10px" }}>
              <Typography variant="h5" component="div" gutterBottom textAlign="center">
                Avanger
              </Typography>
              {/* Hình ảnh thumbnail */}
              <CardContent>
                <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" textAlign="center">
                  <CardMedia
                    component="img"
                    alt="Thumbnail"
                    src={movie.thumbnailURL}
                    style={{
                      width: '40%',
                      height: 'auto',
                      [theme.breakpoints.down('sm')]: {
                        height: 140, // Set a fixed height for small screens
                      },
                      maxHeight: "400px"
                    }}
                  />
                </Box>
              </CardContent>

              <CardContent>
                {/* Nút Like và Dislike */}
                {/* Nút Like và Dislike */}
                <Box display="flex" justifyContent="space-between" alignItems="center">
                  <Box>
                    <IconButton color="primary" onClick={() => handleLike(movie.id)}>
                      <ThumbUpOffAltIcon />
                      <Typography variant="body2" color="textSecondary" style={{ marginLeft: '10px' }}>
                        {movie.totalLike} Likes
                      </Typography>
                    </IconButton>
                  </Box>
                  <Box>
                    <IconButton color="secondary" onClick={() => handleDislike(movie.id)}>
                      <ThumbDownIcon />
                      <Typography variant="body2" color="textSecondary" style={{ marginLeft: '10px' }}>
                        {movie.totalDislikes} Dislikes
                      </Typography>
                    </IconButton>
                  </Box>
                </Box>
              </CardContent>
            </Card>
          </div>
        ))}
      </div>


    </Container>
  );
};

export default HomePage;

