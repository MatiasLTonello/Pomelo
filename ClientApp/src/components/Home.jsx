import React, {useEffect, useState} from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

const Home = () => {

  const [users, setUsers] = useState([])

  const getUsers = async () => {
    try {
        const response = await fetch(`https://pomelo-fintech.azurewebsites.net`, {
        method: "GET",
        headers: {
          'Access-Control-Allow-Origin': '*',
        }
      });
      if (response.ok) {
        const users = await response.json()
        setUsers(users);
      } else {
        console.error("No se pudo eliminar el usuario");
      }
    } catch (error) {
      console.error("Error al eliminar el usuario:", error);
    }
  }

  useEffect(()=> {
    getUsers()
  }, [])

  return (
    <TableContainer component={Paper}>
    <Table sx={{ minWidth: 650 }} aria-label="simple table">
      <TableHead>
        <TableRow>
          <TableCell align="right">ID</TableCell>
          <TableCell align="right">Name</TableCell>
          <TableCell align="right">Surname</TableCell>
          <TableCell align="right">Gender</TableCell>
          <TableCell align="right">Nationality</TableCell>
          <TableCell align="right">Phone</TableCell>
          <TableCell align="right">Status</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {users.map((user) => (
          <TableRow
            key={user.id}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
          >
            <TableCell align="right">{user?.id}</TableCell>
            <TableCell align="right">{user?.name}</TableCell>
            <TableCell align="right">{user?.surname}</TableCell>
            <TableCell align="right">{user?.gender}</TableCell>
            <TableCell align="right">{user?.nationality}</TableCell>
            <TableCell align="right">{user?.phone}</TableCell>
            <TableCell align="right">{user?.status}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </TableContainer>
  )
}

export default Home