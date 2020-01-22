<?php
$db_servername = "127.0.0.1";
$db_username = "root";
$db_password = "";
$db_name = "spacelogins_db";

$conn = new mysqli($db_servername, $db_username, $db_password, $db_name);
if ($conn->connect_error)
{
  die("conection failed ".$conn->connect_error);
}
 ?>
