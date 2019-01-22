<?php

$conn =mysqli_connect('studmysql01.fhict.local', 'dbi307047','123456789','dbi307047');

if(!$conn){
    die("Connection failed: ".mysqli_connect_error());
}