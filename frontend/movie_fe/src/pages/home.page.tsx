import { Box, Button, Card, CardContent, CardMedia, Container, IconButton, Typography, useTheme } from '@mui/material';
import ThumbUpOffAltIcon from '@mui/icons-material/ThumbUpOffAlt';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import { Label } from '@mui/icons-material';
import { useState } from 'react';

const HomePage = () => {
  const theme = useTheme();
  const [likes, setLikes] = useState(0);
  const [dislikes, setDislikes] = useState(0);

  const handleLike = () => {
    setLikes((prevLikes) => prevLikes + 1);
  };

  const handleDislike = () => {
    setDislikes((prevDislikes) => prevDislikes + 1);
  };

  return (
    <Container fixed style={{ paddingBottom: '60px', paddingTop: '50px' }}>

      <Card style={{ border: '1px solid #ccc' }}>
        <Typography variant="h5" component="div" gutterBottom>
          Avanger
        </Typography>
        {/* Hình ảnh thumbnail */}
        <CardMedia
          component="img"
          alt="Thumbnail"
          src="https://source.unsplash.com/random"
          style={{
            width: '100%',
            height: 'auto',
            [theme.breakpoints.down('sm')]: {
              height: 140, // Set a fixed height for small screens
            },
          }}
        />

        <CardContent>
          {/* Nút Like và Dislike */}
          {/* Nút Like và Dislike */}
          <Box display="flex" justifyContent="space-between" alignItems="center">
            <Box>
              <IconButton color="primary" onClick={handleLike}>
                <ThumbUpOffAltIcon />
                <Typography variant="body2" color="textSecondary" style={{marginLeft: '10px'}}>
                  {likes} Likes
                </Typography>
              </IconButton>
            </Box>
            <Box>
              <IconButton color="secondary" onClick={handleDislike}>
                <ThumbDownIcon />
                <Typography variant="body2" color="textSecondary" style={{marginLeft: '10px'}}>
                  {dislikes} Dislikes
                </Typography>
              </IconButton>
            </Box>
          </Box>
        </CardContent>
      </Card>
    </Container>
  );
};

export default HomePage;

