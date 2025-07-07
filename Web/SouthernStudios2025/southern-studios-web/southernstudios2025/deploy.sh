echo "Switching to branch master"
git checkout master

echo "building app..."
npm install
npm run build

echo "Deploying files to server"
scp -r /home/gmaggio/RiderProjects/SouthernStudios2025/Web/SouthernStudios2025/southern-studios-web/southernstudios2025/build/* gmaggio@192.168.1.144:/var/www/192.168.1.144/

echo "done!"

