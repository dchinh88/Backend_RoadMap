import { useState, useEffect } from 'react'
import './App.css';

const genders = [
    {
        id: 1,
        name: 1
    },
    {
        id: 2,
        name: 0
    }
]

function App() {
    const [id, setId] = useState('');
    const [name, setName] = useState('');
    const [birthday, setBirthday] = useState('');
    const [gender, setGender] = useState('');
    const [specialized, setSpecialized] = useState('');
    const [students, setStudents] = useState([]);
    const [khoa, setKhoa] = useState([]);

    const [search, setSearch] = useState('');
    const [ids, setIds] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [paging, setPaging] = useState([])

    const apiStudent = 'https://localhost:7273/api/Sinhviens';
    const apiKhoa = 'https://localhost:7273/api/Khoas';
    const apiGetAll = 'https://localhost:7273/getall';
    const totalPage = Math.ceil(paging.length / 5);
    const listPages = []

    for (let i = 1; i <= totalPage; i++) {
        listPages.push(i)
    }

    useEffect(() => {
        fetch(apiGetAll)
            .then(response => response.json())
            .then(all => setPaging(all))
    }, [students])

    useEffect(() => {
        fetch(`https://localhost:7273/api/Sinhviens?pageNumber=${pageNumber}`)
            .then(response => response.json())
            .then(students => setStudents(students))
    }, [pageNumber]);

    useEffect(() => {
        fetch(apiKhoa)
            .then(response => response.json())
            .then(khoa => setKhoa(khoa))
    }, []);

    const searchStudents = (students) => {
        if (search === '') {
            return students;
        }
        else {
            return students.filter(student => student.tensv.toLowerCase().includes(search.toLowerCase()));
        }
    }

    const student = {
        masv: Number(id),
        tensv: name,
        ngaysinh: birthday,
        gioitinh: Boolean(Number(gender)),
        makhoa: Number(specialized),
        // makhoaNavigation: null
    }

    const handleAddStudent = (student) => {
        fetch(apiStudent, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(student)
        })
            .then(response => response.json())
            .then(student => {
                setStudents([...students, student])

            })
            .catch(error => console.log(error.message))
        resetForm();
    }

    const HandleEditStudent = (id) => {
        fetch(apiStudent + '/' + id)
            .then(response => response.json())
            .then(student => {
                const selectGender = () => {
                    student.gioitinh === true ? document.getElementById('1').checked = true : document.getElementById('0').checked = true
                }
                setId(student.masv)
                setName(student.tensv)
                setBirthday(student.ngaysinh)
                selectGender()
                setGender(student.gioitinh)
                setSpecialized(document.getElementById('selectedMJ').value = student.makhoa)
            })
        console.log(listPages);
    }




    const handleUpdateStudent = (student) => {
        fetch(`https://localhost:7273/api/Sinhviens/${id}`, {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(student)
        })
            .then(response => response.json())
            .then(setStudents(students.map((updateStudent) => {
                if (updateStudent.masv === id) {
                    updateStudent.tensv = name
                    updateStudent.ngaysinh = birthday
                    updateStudent.gioitinh = Boolean(Number(gender))
                    updateStudent.makhoa = Number(specialized)
                    updateStudent.makhoaNavigation = null
                }
                return updateStudent
            }))
            )
            .catch(error => {
                if (error.message === 'aa') {
                    //
                } else {
                    //
                }
            })
        resetForm();
    }

    const handleDeleteStudent = (id) => {
        if (window.confirm('Bạn có chắc muốn xóa sinh viên này ?????')) {
            fetch(`https://localhost:7273/api/Sinhviens/${id}`, {
                method: "DELETE",
            })
                .then(response => response.json())
                .then(setStudents(students.filter(student => student.masv !== id)))
                .catch(error => {
                    if (error.message === 'aa') {
                        //
                    } else {
                        //
                    }
                })
        }
    }

    const handleDeleteClick = () => {
        if (window.confirm('Bạn có chắc muốn xóa những sinh viên này ?????')) {
            ids.forEach(id => {
                fetch(`https://localhost:7273/api/Sinhviens/${id}`, {
                    method: "DELETE",
                })
                    .then(response => response.json())
                    // .then(response => console.log(response))
                    .then(setStudents(students.filter(student => !id.includes(student.masv))))
                    .catch(error => {
                        if (error.message === 'aa') {
                            //
                        } else {
                            //
                        }
                    })
            })
        }
        setIds([])
    }


    const onChange = (e) => {
        const id = e.target.value;
        if (e.target.checked) {
            setIds([...ids, id]);
        } else {
            setIds(ids.filter(i => i !== id));
        }
    }

    const resetForm = () => {
        setId('')
        setName('')
        setBirthday('')
        setGender(student.gioitinh === false ? document.getElementById('0').checked = false : document.getElementById('1').checked = false)
        setSpecialized('')
    }

    const previousPage = () => {
        setPageNumber(() => pageNumber - 1)
        if (pageNumber <= 1) setPageNumber(1)
    }

    const nextPage = () => {
        setPageNumber(pageNumber + 1)
        if (pageNumber >= totalPage) setPageNumber(totalPage)
    }

    return (
        <div className="App">
            <div className="FormSearch">
                <h1>Student Managerment</h1>
                <div className="search">
                    <label className="searchLabel">Search </label>
                    <input
                        type="text"
                        className="txtsearch"
                        placeholder="Enter name"
                        value={search}
                        onChange={e => setSearch(e.target.value)}
                    />
                    {/* <button className='btnSearch'>Search</button> */}
                </div>
            </div>
            <div className="FormInput">
                <div className="idInput">
                    <label>Enter ID</label>
                    <input
                        value={id}
                        onChange={e => setId(e.target.value)}
                        className="txtId"
                        type="text"
                        placeholder="Enter ID"
                    />

                </div>
                <div className="nameInput">
                    <label>Enter Name</label>
                    <input
                        value={name}
                        onChange={e => setName(e.target.value)}
                        type="text"
                        className="txtName"
                        placeholder="Enter Name"
                    />

                </div>
                <div className="birthdayInput">
                    <label>Select Birthday</label>
                    <input
                        value={birthday}
                        onChange={e => setBirthday(e.target.value)}
                        className="selectedDate"
                        type="datetime-local"
                    />
                </div>
                <div className="selectGender">
                    <label className='labelGender'>Select Gender</label>
                    {genders.map(gender => (
                        <div key={gender.id}>
                            <input
                                type='radio'
                                id={String(gender.name)}
                                name='student-gender'
                                className='selectedGender'
                                value={gender.name}
                                onChange={e => setGender(e.target.value)}
                            />
                            {gender.name === 1 ? 'Nam' : 'Nu'}
                        </div>
                    ))}
                </div>
                <div className="selectSpecializeds">
                    <label className='labelSpecializeds'>Select Specialized</label>
                    <select
                        name="student-specialized"
                        className='selectedSpecialized'
                        id='selectedMJ'
                        onChange={e => setSpecialized(e.target.value)}
                    >
                        {khoa.map((specialized, index) => (
                            <option
                                key={index}
                                value={specialized.makhoa}
                            >
                                {specialized.tenkhoa}
                            </option>
                        ))}
                    </select>
                </div>
            </div>
            <div className="FormAction">
                <div className="addStudent">
                    <button
                        className='btnAdd'
                        onClick={() => handleAddStudent(student)}
                    >
                        ADD
                    </button>
                </div>
                <div className="updateStudent">
                    <button
                        className='btnUpdate'
                        onClick={() => handleUpdateStudent(student)}
                    >UPDATE</button>
                </div>
                <div className="deleteStudent">
                    <button
                        className='btnDelete'
                        onClick={handleDeleteClick}
                    >
                        DELETE
                    </button>
                </div>
            </div>
            <div className="studentsList">
                <table className="list">
                    <thead>
                        <tr>
                            <td>(*)</td>
                            <td>Mã sinh viên</td>
                            <td>Tên sinh viên</td>
                            <td>Ngày sinh</td>
                            <td>Giới tính</td>
                            <td>Ngành học</td>
                            <td>Action</td>
                        </tr>
                    </thead>
                    <tbody id='form-list-student-body'>
                        {searchStudents(students).map((student) => (
                            <tr key={student.masv}>
                                <td>
                                    <input
                                        id={`st-${student.masv}`}
                                        type='checkbox'
                                        value={student.masv}
                                        onChange={onChange}
                                    />
                                </td>
                                <td>{student.masv}</td>
                                <td>{student.tensv}</td>
                                <td>{student.ngaysinh}</td>
                                <td>{student.gioitinh === true ? "Nam" : "Nu"}</td>
                                <td>{khoa.map(k => student.makhoa === k.makhoa ? k.tenkhoa : '')}</td>
                                <td>
                                    <button onClick={() => HandleEditStudent(student.masv)}>EDIT</button>
                                    <button onClick={() => handleDeleteStudent(student.masv)} >DELETE</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <div className='pagination'>
                    <div className='previous'>
                        <button className='btn-previous' onClick={previousPage}>Previous</button>
                    </div>
                    <div className='listPage'>
                        {
                            listPages.map((i) => (
                                <p 
                                    key={i} 
                                    onClick={() => setPageNumber(i)}
                                    className={pageNumber===i ? 'highlight' : ''}
                                >
                                    {i}
                                </p>
                            ))
                        }

                    </div>
                    <div className='next'>
                        <button className='btn-next' onClick={nextPage}>Next</button>
                    </div>

                </div>
            </div>
        </div>
    );
}

export default App;
